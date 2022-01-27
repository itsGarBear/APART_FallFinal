using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageCanvasSpawner : MonoBehaviour
{
    public TextMeshProUGUI damageTextPrefab;
    public static DamageCanvasSpawner instance;
    public Camera cam;

    Color inVulnColor = new Color32(255, 195, 0, 255);
    Color lethalColor = Color.red;
    Color nonLethalColor = new Color32(127, 88, 219, 255);

    private void Awake()
    {
        instance = this;
    }

    public void SpawnDamageText(int damage, Vector3 pos, int textColor)
    {
        Vector3 screenPos = cam.WorldToScreenPoint(pos);

        TextMeshProUGUI damageText = Instantiate(damageTextPrefab, screenPos, Quaternion.identity, this.transform);

        if (textColor == 0)
        {
            print("invul");
            damageText.color = inVulnColor;
        }
        if (textColor == 1)
        {
            print("lethal");
            damageText.color = lethalColor;
        }
        if (textColor == 2)
        {
            print("nonlethal");
            damageText.color = nonLethalColor;
        }

        damageText.text = damage.ToString();
        damageText.gameObject.GetComponent<Animator>().SetTrigger("Play");
        //SegmentedHealthBar.instance.UpdateHealthBar(damage);
        
    }
}
