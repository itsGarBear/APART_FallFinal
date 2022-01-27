using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Move Tutorial: https://pavcreations.com/platformers-implementation-in-unity-from-scratch/3/#Player-controller-script
    //Ladder Tutorial: https://pavcreations.com/climbing-ladders-mechanic-in-unity-2d-platformer-games/ 
    //SNAPPY JUMPS https://pavcreations.com/jumping-controls-in-2d-pixel-perfect-platformers/

    [Header("Player State")]
    [HideInInspector] public static PlayerController myPlayer;
    public bool isCara = false;
    Player player;

    [Header("Movement")]
    public float maxMoveSpeed = 50f;
    public float moveSpeed;
    public float jumpForce = 5f;
    public float climbSpeed = .2f;

    private float xMove = 0f;
    private float yMove = 0f;
    private float lastJumpY = 0f;
    private bool isFacingRight = false;
    private bool isJumping = false;

    //ladder
    private Transform ladder;
    private bool jumpHeld = false;
    private bool crouchHeld = false;
    private bool isUnderPlatform = false;
    public bool isCloseToLadder = false;
    public bool climbHeld = false;
    public bool hasStartedClimb = false;

    //stairs
    private Stairs stair;
    public bool isNearAStair = false;

    [Header("Weapon")]
    public bool canShoot = true;

    [Header("Components")]
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    public CircleCollider2D circleCollider;
    public SpriteRenderer activeBodySprite;
    public SpriteRenderer activeArmSprite;
    public Animator meleeAnim;

    //delete me later
    public Toggle creativeToggle;

    [Header("Cara Sprites")]
    public Sprite cBodySprite;
    public Sprite cArmSprite;

    [Header("Alice Sprites")]
    public Sprite aBodySprite;
    public Sprite aArmSprite;

    //Arm transform positions
    public Transform armLeftSide;
    public Transform armRightSide;
    public GameObject arm;

    //Flip character stuff
    public Vector3 mouseInput;
    public Animator anim;
    public bool isFlipped = false;

    [Header("Porqins Collide")]
    public int porqDamage = 4;
    private float timer = 0f;
    private float timeBetweenDamage = 3f;

    public void toggleGravity()
    {
        if (!creativeToggle.isOn)
            rb.gravityScale = 1;
        else
            rb.gravityScale = 0;
    }
    private void Awake()
    {
        myPlayer = this;
        rb = GetComponentInChildren<Rigidbody2D>();
        circleCollider = GetComponentInChildren<CircleCollider2D>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        player = GetComponent<Player>();
        activeBodySprite.sprite = cBodySprite;
        activeArmSprite.sprite = cArmSprite;
    }
    private void Start()
    {
        moveSpeed = maxMoveSpeed;
    }

    private void Update()
    {
        if(!TimeScaler.instance.timeIsStopped)
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                moveSpeed = maxMoveSpeed;
            }

            #region Creative Mode
            if (Input.GetKeyDown(KeyCode.C))
            {
                creativeToggle.isOn = !creativeToggle.isOn;
            }

            if (!creativeToggle.isOn)
            {
                boxCollider.enabled = true;
                circleCollider.enabled = true;
                rb.gravityScale = 1;
                Move();

                if (IsGrounded() && Input.GetButtonDown("Jump"))
                    isJumping = true;

                if (!IsGrounded())
                {
                    if (lastJumpY < transform.position.y)
                        lastJumpY = transform.position.y;
                }
            }
            else
            {
                boxCollider.enabled = false;
                circleCollider.enabled = false;
                rb.gravityScale = 0;
                CreativeMove();
            }
            #endregion

            yMove = Input.GetAxisRaw("Vertical") * climbSpeed;

            //crouchHeld = (IsGrounded() && !isCloseToLadder && Input.GetButton("Crouch")) ? true : false;
            climbHeld = (isCloseToLadder && Input.GetButton("Climb")) ? true : false;
            if (climbHeld)
            {
                if (!hasStartedClimb) hasStartedClimb = true;
            }

            //Enter Stair
            //if (isNearAStair && Input.GetButtonDown("Climb"))
            //{
            //    transform.position = stair.EnterStair();
            //    print(stair.name);
            //    stair = stair.stairExitsAt;
            //    print(stair.name);
            //    isNearAStair = true;
            //}

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            mouseInput = Camera.main.ScreenToWorldPoint(mousePos);

            if (mouseInput.x < transform.position.x)
            {
                isFlipped = true;
                arm.transform.position = armRightSide.position;
            }
            else
            {
                isFlipped = false;
                arm.transform.position = armLeftSide.position;
            }

            activeBodySprite.flipX = isFlipped;
            activeArmSprite.flipX = isFlipped;

            anim.SetFloat("xInput", mouseInput.x);
            anim.SetFloat("zInput", mouseInput.z);
        }
        
    }
    public void Melee()
    {
        if(!isFlipped)
        {
            meleeAnim.SetBool("MeleeFlipped", true);
        }
        else
        {
            meleeAnim.SetBool("Melee", true);
        }
    }
    public void SwitchCharacterSprites()
    {
        if(isCara)
        {
            activeBodySprite.sprite = cBodySprite;
            activeArmSprite.sprite = cArmSprite;
        }
        else
        {
            activeBodySprite.sprite = aBodySprite;
            activeArmSprite.sprite = aArmSprite;
        }
    }
    private void FixedUpdate()
    {
        if(!creativeToggle.isOn)
        {
            float moveFactor = xMove * Time.fixedDeltaTime;

            rb.velocity = new Vector2(moveFactor * 10f, rb.velocity.y);

            //if (moveFactor > 0 && !isFacingRight) flipFacingDirection();
            //else if (moveFactor < 0 && isFacingRight) flipFacingDirection();

            if (isJumping)
            {
                Jump();
            }

            #region Climbing

            if (hasStartedClimb && !climbHeld)
            {
                //if (xMove > 0 || xMove < 0) 
                ResetClimbing();
            }
            else if (hasStartedClimb && climbHeld)
            {
                float height = GetComponent<SpriteRenderer>().size.y;
                float topLadderY = 0f;
                float bottomLadderY = 0f;
                try
                {
                    topLadderY = Half(ladder.transform.GetChild(0).transform.position.y + height);
                    bottomLadderY = Half(ladder.transform.GetChild(1).transform.position.y - height);
                }
                catch
                {
                    //print("i have crippling depression");
                }


                float transformY = Half(transform.position.y);
                float transformVY = transformY + yMove;

                if (transformVY > topLadderY || transformVY < bottomLadderY)
                {
                    ResetClimbing();
                }
                else if (transformY <= topLadderY && transformY >= bottomLadderY)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    if (!transform.position.x.Equals(ladder.transform.position.x))
                        transform.position = new Vector3(ladder.transform.position.x, transform.position.y, transform.position.z);

                    Vector3 forwardDir = new Vector3(0f, transformVY, 0);
                    Vector3 newPos = Vector3.zero;

                    if (yMove > 0)
                    {
                        if(forwardDir.y <= 0)
                            newPos = transform.position - forwardDir * Time.deltaTime * climbSpeed;
                        else
                            newPos = transform.position + forwardDir * Time.deltaTime * climbSpeed;
                    }
                    else if (yMove < 0)
                    {
                        if (forwardDir.y <= 0)
                            newPos = transform.position + forwardDir * Time.deltaTime * climbSpeed;
                        else
                            newPos = transform.position - forwardDir * Time.deltaTime * climbSpeed;
                    }
                        
                    if (newPos != Vector3.zero)
                        rb.MovePosition(newPos);
                }
            }

            #endregion
        }

    }

    private void ResetClimbing()
    {
        //if(hasStartedClimb)
        //{
            hasStartedClimb = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.position = new Vector3(transform.position.x, Half(transform.position.y), transform.position.z);
        //}
    }

    public static float Half(float value)
    {
        //print(Mathf.Floor(value) + 0.5f);
        return Mathf.Floor(value) + 0.5f;
    }
    private void flipFacingDirection()
    {
        isFacingRight = !isFacingRight;

        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }
    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        isJumping = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(circleCollider.bounds.center, circleCollider.radius, Vector2.down, 0.1f, groundLayer);
        if (hit && !lastJumpY.Equals(0))
            lastJumpY = 0;

        return hit.collider != null;
    }
    private void CreativeMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(x,y,0f) * moveSpeed;
    }

    private void Move()
    {
        xMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        Vector2 dir = (transform.right * xMove);
        dir.y = rb.velocity.y;

        rb.velocity = dir;

    }

    private void TryJump()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        //RaycastHit2D hit;
        if (Physics2D.Raycast(transform.position, -Vector2.up, boxCollider.bounds.extents.y + 0.1f, groundLayer.value))
        {
            
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Boss")
        {
            if(timer <= timeBetweenDamage)
            {
                timer += Time.deltaTime;
                if(timer > timeBetweenDamage)
                {
                    player.Damage(porqDamage);
                    timer = 0f;
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stairs"))
        {
            isNearAStair = true;
            stair = collision.gameObject.GetComponent<Stairs>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ladder"))
        {
            isCloseToLadder = true;
            this.ladder = collision.transform;
        }

        if(collision.gameObject.CompareTag("Stairs"))
        {

        }
 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ladder"))
        {
            isCloseToLadder = false;
            this.ladder = null;
        }

        if (collision.gameObject.CompareTag("Stairs"))
        {
            isNearAStair = false;
            stair = null;
        }
    }
}
