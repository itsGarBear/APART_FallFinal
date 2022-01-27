using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DoorLevel
{
    Level1,
    Level2,
    Level3
}

public class DoorSecurity : MonoBehaviour
{
    //Door info
    public DoorLevel level;
    private float openRadius = 5f;
    private GameObject doorModel;

    //Calls
    private Inventory playerInv;
    private Player player;

    //Text
    public TextMeshProUGUI doorOpenRequest;
    public TextMeshProUGUI doorLocked;

    private bool canUpdate = false;

    private void Awake()
    {
        doorModel = this.gameObject;
        doorOpenRequest.enabled = false;
        doorLocked.enabled = false;

        doorLocked.text = "You need a " + level + " card to access this door";
        doorOpenRequest.text = "Press \"E\" to unlock door";
    }

    private void Start()
    {
        Invoke("FindDependencies", 5f);
    }

    public void FindDependencies()
    {
        player = FindObjectOfType<Player>();
        playerInv = FindObjectOfType<Inventory>();
        canUpdate = true;
    }

    private void Update()
    {
        if(canUpdate)
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) < openRadius && Mathf.Abs(transform.position.y - player.transform.position.y) < openRadius)
            {
                doorOpenRequest.enabled = true;
                print("You've approached the door");

                if (Input.GetKeyDown("e"))
                {
                    if (level == DoorLevel.Level1 && playerInv.hasLevel1)
                    {
                        OpenDoor();
                    }
                    else if (level == DoorLevel.Level2 && playerInv.hasLevel2)
                    {
                        OpenDoor();
                    }
                    else if (level == DoorLevel.Level3 && playerInv.hasLevel3)
                    {
                        OpenDoor();
                    }
                    else
                    {
                        doorLocked.enabled = true;
                        doorOpenRequest.text = "";
                        Invoke("DisableText", 2);
                        Invoke("EnableText", 2.1f);
                    }
                }
            }
            else
            {
                doorOpenRequest.enabled = false;
            }
        }
        
    }

    public void OpenDoor()
    {
        doorModel.SetActive(false);
    }

    private void DisableText()
    {
        doorLocked.enabled = false;
    }

    private void EnableText()
    {
        doorOpenRequest.text = "Press \"E\" to unlock door";
    }
}
