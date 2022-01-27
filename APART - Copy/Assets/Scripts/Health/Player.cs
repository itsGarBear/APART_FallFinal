using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Material material;
    private Color materialTintColor;

    [SerializeField] private Inventory inv;
    [SerializeField] PlayerWeapon playerW;

    public void Heal(int healAmount)
    {
        HeartsVisual.heartsHealthSystemStatic.Heal(healAmount);
    }

    private void DamageFlash()
    {
        foreach(SpriteRenderer sR in PlayerController.myPlayer.GetComponents<SpriteRenderer>())
        {
            sR.color = Color.red;
        }
        Invoke("NormalColor", .5f);
    }
    private void NormalColor()
    {
        foreach(SpriteRenderer sR in PlayerController.myPlayer.GetComponents<SpriteRenderer>())
        {
            sR.color = Color.white;
        }
    }



    public void Damage(int damageAmount)
    {
        DamageFlash();
        HeartsVisual.heartsHealthSystemStatic.Damage(damageAmount);
    }

    private void Update()
    {
        if(!TimeScaler.instance.timeIsStopped)
        {
            if (!playerW.isReloading)
            {
                if (Input.GetKeyDown("1"))
                {
                    inv.UpdateSlotSelection(0);
                }
                if (Input.GetKeyDown("2"))
                {
                    inv.UpdateSlotSelection(1);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    inv.DropItem();
                }
            }
        }
        
        if (inv.hasLevel1)
        {
            inv.level1Card.enabled = true;
        }
        else
        {
            inv.level1Card.enabled = false;
        }

        if (inv.hasLevel2)
        {
            inv.level2Card.enabled = true;
        }
        else
        {
            inv.level2Card.enabled = false;
        }

        if (inv.hasLevel3)
        {
            inv.level3Card.enabled = true;
        }
        else
        {
            inv.level3Card.enabled = false;
        }
    }
}
