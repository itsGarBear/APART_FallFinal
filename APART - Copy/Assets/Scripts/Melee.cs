using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    SegmentedHealthBar segmentedHealthBar;

    public int bossHeadShotDamage = 5;
    public int bossBodyShotDamage = 3;
    public int bossLegShotDamage = 2;

    public void ResetMelee(string name)
    {
        playerWeapon.meleeAnim.SetBool(name, false);
        playerWeapon.canMelee = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PlayerController.myPlayer.isCara)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == "Boss")
            {
                segmentedHealthBar = collision.gameObject.transform.root.GetComponentInChildren<SegmentedHealthBar>();

                if (collision.gameObject.CompareTag("Head"))
                {
                    if (segmentedHealthBar.isInvulnerable)
                    {
                        //invulnerable color = 0, lethal = 1, nonlethal = 2
                        DamageCanvasSpawner.instance.SpawnDamageText(bossHeadShotDamage, collision.contacts[0].point, 0);
                    }
                    else
                    {
                        DamageCanvasSpawner.instance.SpawnDamageText(bossHeadShotDamage, collision.contacts[0].point, 2);
                        PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, false);
                        segmentedHealthBar.UpdateHealthBar(bossHeadShotDamage, false);

                    }

                }
                else if (collision.gameObject.CompareTag("Body"))
                {
                    if (segmentedHealthBar.isInvulnerable)
                    {
                        //invulnerable color = 0, lethal = 1, nonlethal = 2
                        DamageCanvasSpawner.instance.SpawnDamageText(bossBodyShotDamage, collision.contacts[0].point, 0);
                    }
                    else
                    {
                        DamageCanvasSpawner.instance.SpawnDamageText(bossBodyShotDamage, collision.contacts[0].point, 2);
                        PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, false);
                        segmentedHealthBar.UpdateHealthBar(bossBodyShotDamage, false);

                    }

                }
                else if (collision.gameObject.CompareTag("FrontLegs"))
                {

                    if (segmentedHealthBar.isInvulnerable)
                    {
                        //invulnerable color = 0, lethal = 1, nonlethal = 2
                        DamageCanvasSpawner.instance.SpawnDamageText(bossLegShotDamage, collision.contacts[0].point, 0);
                    }
                    else
                    {

                        DamageCanvasSpawner.instance.SpawnDamageText(bossLegShotDamage, collision.contacts[0].point, 2);
                        PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, false);
                        segmentedHealthBar.UpdateHealthBar(bossLegShotDamage, false);


                    }

                }
                else if (collision.gameObject.CompareTag("BackLegs"))
                {

                    if (segmentedHealthBar.isInvulnerable)
                    {
                        //invulnerable color = 0, lethal = 1, nonlethal = 2
                        DamageCanvasSpawner.instance.SpawnDamageText(bossLegShotDamage, collision.contacts[0].point, 0);
                    }
                    else
                    {

                        DamageCanvasSpawner.instance.SpawnDamageText(bossLegShotDamage, collision.contacts[0].point, 2);
                        PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, false);
                        segmentedHealthBar.UpdateHealthBar(bossLegShotDamage, false);

                    }

                }
            }
            else if(collision.gameObject.CompareTag("Rat"))
            {
                collision.gameObject.GetComponent<Rat>().enabled = false;
                collision.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                Destroy(collision.gameObject, 2f);
            }
            else if(collision.gameObject.CompareTag("Rabbit"))
            {
                collision.gameObject.GetComponent<Rabbit>().enabled = false;
                collision.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                Destroy(collision.gameObject, 2f);
            }
        }

        else if (PlayerController.myPlayer.isCara)
        {
            if (collision.gameObject.CompareTag("SecurityCamera"))
            {
                print("camera disabled");
                collision.gameObject.transform.parent.parent.GetComponent<SecurityCamera>().isDisabled = true;
            }
            else if (collision.gameObject.CompareTag("Guard"))
            {
                print("guard disabled");
                collision.gameObject.GetComponent<Guard>().enabled = false;
                collision.gameObject.transform.Find("ArmPivot").Find("Arm").GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                Destroy(collision.gameObject, 2f);
            }
        }
    }
}
