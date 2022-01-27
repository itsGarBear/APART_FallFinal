using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Pistol Ammo")]
    //Pistol Ammo
    private int pistolMaxAmmo = 30;
    private int pistolMaxClip = 10;
    private int pistolCurrentAmmo = 10;
    private int pistolTotalAmmo = 30;

    [Header("Tranq Ammo")]
    //Tranq Ammo
    private int tranqMaxAmmo = 10;
    private int tranqMaxClip = 1;
    private int tranqCurrentAmmo = 1;
    private int tranqTotalAmmo = 10;
    
    public List<TextMeshProUGUI> slotAmmoText;

    [Header("Pistol")]
    //Gun
    private float pistolBulletSpeed = 2f;
    private float pistolShootRate = .2f;
    private float pistolReloadTime = 2f;

    //Tranq
    [Header("Tranq")]
    private float tranqDartSpeed = 1.75f;
    private float tranqShootRate = 2f;
    private float tranqRelaodTime = 4f;

    private float lastShootTime;
    private float maxSpread = .1f;

    //Melee
    [Header("Melee")]
    public Animator meleeAnim;
    public bool canMelee = true;

    //Extra
    [Header("Extra")]
    public GameObject bulletPrefab;
    public GameObject dartPrefab;
    public Transform bulletSpawnPos;
    
    public AudioSource audioSource;

    //Pistol Audio
    public AudioClip pistolShotSound;
    public AudioClip pistolReloadSound;

    //Tranq Audio
    public AudioClip tranqShotSound;

    private PlayerController pc;
    public Inventory playerInv;

    public bool isReloading = false;
    public float timer = 0f;

    private EscapeManager eM;

    private void Awake()
    {
        UpdateAmmoText();
        pc = FindObjectOfType<PlayerController>();
        TryShoot();
        eM = FindObjectOfType<EscapeManager>();
    }

    public void Update()
    {
        if(!TimeScaler.instance.timeIsStopped)
        {
            if (!isReloading)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (playerInv.currentEquipped != 1 && playerInv.currentEquipped != 2)
                    {
                        if (canMelee)
                            Melee();
                    }
                    if (pc.canShoot)
                        TryShoot();
                }

                else if (Input.GetKeyDown(KeyCode.R))
                {
                    Reload2();
                    return;
                }

                if (playerInv.currentEquipped == 1 || playerInv.currentEquipped == 2)
                {
                    slotAmmoText[playerInv.currentSlot].enabled = true;
                }
                else
                {
                    slotAmmoText[playerInv.currentSlot].enabled = false;
                }
            }
            else
            {
                if (timer < playerInv.reloadSliders[playerInv.currentSlot].maxValue)
                {
                    timer += Time.deltaTime;
                    playerInv.reloadSliders[playerInv.currentSlot].value = timer;
                }
                else
                {
                    timer = 0f;
                    isReloading = false;
                    playerInv.reloadSliders[playerInv.currentSlot].value = 0f;
                }
            }
        }
    }
    public void Melee()
    {
        canMelee = false;
        PlayerController.myPlayer.Melee();
    }


    public void TryShoot()
    {
        if ((playerInv.currentEquipped == 1 && !playerInv.selected) && pistolCurrentAmmo <= 0 || Time.time - lastShootTime < pistolShootRate)
        { 
            return;
        }
        
        if ((playerInv.currentEquipped == 2 && !playerInv.selected) && tranqCurrentAmmo <= 0 || Time.time - lastShootTime < tranqShootRate)
        {
            return;
        }

        if ((playerInv.currentEquipped != 1 || playerInv.currentEquipped != 2) && !playerInv.selected)
        {
            return;
        }

        if (eM.isPaused)
        {
            return;
        }
        
        lastShootTime = Time.time;

        Quaternion angle = bulletSpawnPos.transform.parent.transform.parent.rotation;
        angle.eulerAngles = new Vector3(angle.eulerAngles.x, angle.eulerAngles.y, angle.eulerAngles.z + 180f);
        if (playerInv.currentEquipped == 1)
        {
            pistolCurrentAmmo--;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, angle);
            Bullet b = bullet.GetComponent<Bullet>();

            b.rb.velocity = bulletSpawnPos.right * b.fireForce;

            audioSource.clip = pistolShotSound;
            audioSource.Play();
        }
        else if (playerInv.currentEquipped == 2)
        {
            tranqCurrentAmmo--;
            GameObject bullet = Instantiate(dartPrefab, bulletSpawnPos.position, angle);
            Bullet b = bullet.GetComponent<Bullet>();

            b.rb.velocity = bulletSpawnPos.right * b.fireForce;

            audioSource.clip = tranqShotSound;
            audioSource.Play();
        }

        UpdateAmmoText();

    }

    public void GiveAmmo(int ammoToGive)
    {
        if (playerInv.currentEquipped == 1)
        {
            pistolTotalAmmo = Mathf.Clamp(pistolTotalAmmo + ammoToGive, 0, pistolMaxAmmo);
        }
        else if (playerInv.currentEquipped == 0)
        {
            tranqTotalAmmo = Mathf.Clamp(tranqTotalAmmo + ammoToGive, 0, tranqMaxAmmo);
        }
        UpdateAmmoText();
    }

    public void Reload()
    {
        if (playerInv.currentEquipped == 1)
        {
            for (int i = pistolCurrentAmmo; i < pistolMaxClip; i++)
            {
                pistolCurrentAmmo++;
                pistolTotalAmmo--;
 
            }
        }
        else if (playerInv.currentEquipped == 2)
        {
            print("reloading tranq");
            for (int i = tranqCurrentAmmo; i < tranqMaxClip; i++)
            {
                tranqCurrentAmmo++;
                tranqTotalAmmo--;   
            }
        }
        UpdateAmmoText();
    }

    public void Reload2()
    {
        if (playerInv.currentEquipped == 1)
        {
            playerInv.reloadSliders[playerInv.currentSlot].maxValue = pistolReloadTime;
            isReloading = true;
            Invoke("Reload", pistolReloadTime);
            Invoke("ReloadSound", pistolReloadTime);
            
        }
        else if (playerInv.currentEquipped == 2)
        {
            playerInv.reloadSliders[playerInv.currentSlot].maxValue = tranqRelaodTime;
            isReloading = true;
            Invoke("Reload", tranqRelaodTime);
        }
    }

    public void ReloadSound()
    {
        audioSource.clip = pistolReloadSound;
        audioSource.Play();
    }
    public void UpdateAmmoText()
    {
        if (playerInv.currentEquipped == 1)
        {
            slotAmmoText[playerInv.currentSlot].text = pistolCurrentAmmo + "/" + pistolTotalAmmo;
        }
        else if (playerInv.currentEquipped == 2)
        {
            slotAmmoText[playerInv.currentSlot].text = tranqCurrentAmmo + "/" + tranqTotalAmmo;
        }
        
    }
}
