using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PickupTypes
{
    Ammo,
    Health,
    Card
}

public class RadiusDetector : MonoBehaviour
{
    public float pickupRadius = 3f;
    private Player player;
    private Inventory playerInv;
    public List<TextMeshProUGUI> pickupText;
    public int value;
    public PickupTypes type;
    private bool canUpdate = false;

    private void Start()
    {
        foreach (TextMeshProUGUI text in pickupText)
        {
            text.enabled = false;
        }
        Invoke("FindDependencies", 6);
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
            if (!TimeScaler.instance.timeIsStopped)
            {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) < pickupRadius)
                {
                    foreach (TextMeshProUGUI text in pickupText)
                    {
                        text.enabled = true;
                    }

                    if (Input.GetKeyDown("e"))
                    {
                        if (type == PickupTypes.Ammo)
                        {
                            PlayerWeapon playerW = FindObjectOfType<PlayerWeapon>();
                            playerW.GiveAmmo(value);
                        }

                        if (type == PickupTypes.Health)
                        {
                            player.Heal(value);
                        }

                        if (type == PickupTypes.Card)
                        {
                            if (this.name == "Level1")
                            {
                                playerInv.hasLevel1 = true;
                            }
                            else if (this.name == "Level2")
                            {
                                playerInv.hasLevel2 = true;
                            }
                            else
                            {
                                playerInv.hasLevel3 = true;
                            }
                        }
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    foreach (TextMeshProUGUI text in pickupText)
                    {
                        text.enabled = false;
                    }
                }
            }
        }
        
        
    }
}
