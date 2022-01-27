using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bullet : MonoBehaviour
{
    private int lethalBossHeadShotDamage = 7;
    private int lethalBossBodyShotDamage = 5;
    private int lethalBossLegShotDamage = 3;

    private int nonLethalBossHeadShotDamage = 10;
    private int nonLethalBossBodyShotDamage = 7;
    private int nonLethalBossLegShotDamage = 5;

    public Rigidbody2D rb;
    public float fireForce = 10f;
    SegmentedHealthBar segmentedHealthBar;

    public bool isLethal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
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
                        DamageCanvasSpawner.instance.SpawnDamageText(0, collision.contacts[0].point, 0);
                    }
                    else
                    {
                        if (isLethal)
                        {
                            DamageCanvasSpawner.instance.SpawnDamageText(lethalBossHeadShotDamage, collision.contacts[0].point, 1);
                            PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, true);
                            segmentedHealthBar.UpdateHealthBar(lethalBossHeadShotDamage, true);
                        }
                        else
                        {
                            DamageCanvasSpawner.instance.SpawnDamageText(nonLethalBossHeadShotDamage, collision.contacts[0].point, 2);
                            PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, false);
                            segmentedHealthBar.UpdateHealthBar(nonLethalBossHeadShotDamage, false);
                        }
                        
                    }

                }
                else if (collision.gameObject.CompareTag("Body"))
                {
                    if (segmentedHealthBar.isInvulnerable)
                    {
                        //invulnerable color = 0, lethal = 1, nonlethal = 2
                        DamageCanvasSpawner.instance.SpawnDamageText(0, collision.contacts[0].point, 0);
                    }
                    else
                    {
                        if (isLethal)
                        {
                            DamageCanvasSpawner.instance.SpawnDamageText(lethalBossBodyShotDamage, collision.contacts[0].point, 1);
                            PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, true);
                            segmentedHealthBar.UpdateHealthBar(lethalBossLegShotDamage, true);
                        }
                        else
                        {
                            DamageCanvasSpawner.instance.SpawnDamageText(nonLethalBossBodyShotDamage, collision.contacts[0].point, 2);
                            PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, false);
                            segmentedHealthBar.UpdateHealthBar(nonLethalBossBodyShotDamage, false);
                        }

                    }

                }
                else if (collision.gameObject.CompareTag("FrontLegs"))
                {
                    if (segmentedHealthBar.isInvulnerable)
                    {
                        //invulnerable color = 0, lethal = 1, nonlethal = 2
                        DamageCanvasSpawner.instance.SpawnDamageText(0, collision.contacts[0].point, 0);
                    }
                    else
                    {
                        if (isLethal)
                        {
                            DamageCanvasSpawner.instance.SpawnDamageText(lethalBossLegShotDamage, collision.contacts[0].point, 1);
                            PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, true);
                            segmentedHealthBar.UpdateHealthBar(lethalBossLegShotDamage, true);
                        }
                        else
                        {
                            DamageCanvasSpawner.instance.SpawnDamageText(nonLethalBossLegShotDamage, collision.contacts[0].point, 2);
                            PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, false);
                            segmentedHealthBar.UpdateHealthBar(nonLethalBossLegShotDamage, false);
                        }

                    }

                }
                else if (collision.gameObject.CompareTag("BackLegs"))
                {
                    if (segmentedHealthBar.isInvulnerable)
                    {
                        //invulnerable color = 0, lethal = 1, nonlethal = 2
                        DamageCanvasSpawner.instance.SpawnDamageText(0, collision.contacts[0].point, 0);
                    }
                    else
                    {
                        if (isLethal)
                        {
                            DamageCanvasSpawner.instance.SpawnDamageText(lethalBossLegShotDamage, collision.contacts[0].point, 1);
                            PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, true);
                            segmentedHealthBar.UpdateHealthBar(lethalBossLegShotDamage, true);
                        }
                        else
                        {
                            DamageCanvasSpawner.instance.SpawnDamageText(nonLethalBossLegShotDamage, collision.contacts[0].point, 2);
                            PorqinsManager.instance.DamageFlash(new SpriteRenderer[] { collision.gameObject.GetComponent<SpriteRenderer>() }, false);
                            segmentedHealthBar.UpdateHealthBar(nonLethalBossLegShotDamage, false);
                        }

                    }

                }
            }
            else if(!isLethal)
            {
                if (collision.gameObject.CompareTag("Rat"))
                {
                    print("rat disabled");
                    collision.gameObject.GetComponent<Rat>().enabled = false;
                    collision.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                    Destroy(collision.gameObject, 2f);
                }
                else if (collision.gameObject.CompareTag("Rabbit"))
                {
                    print("rabbit disabled");
                    collision.gameObject.GetComponent<Rabbit>().enabled = false;
                    collision.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                    Destroy(collision.gameObject, 2f);
                }
            }
        }

        else if (PlayerController.myPlayer.isCara)
        {
            if(collision.gameObject.CompareTag("SecurityCamera"))
            {
                print("camera disabled");
                collision.gameObject.transform.parent.parent.GetComponent<SecurityCamera>().isDisabled = true;
            }

            else if (!isLethal)
            {
                if (collision.gameObject.CompareTag("Guard"))
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

}
