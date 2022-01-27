using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SegmentedHealthBar : MonoBehaviour
{
    public static SegmentedHealthBar instance;
    public TextMeshProUGUI damageText;
    public List<Slider> sliders;
    public int maxHealth = 100;
    private int segmentAmount;
    private int segmentNdx = 0;
    private int currHealthSum = 0;
    public bool isInvulnerable = false;

    Color inVulnColor = new Color32(255, 195, 0, 255);
    Color lethalColor = Color.red;
    Color nonLethalColor = new Color32(127, 88, 219, 255);

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        damageText.text = maxHealth.ToString();
        segmentAmount = maxHealth / sliders.Count;
        foreach(Slider s in sliders)
        {
            s.maxValue = segmentAmount;
            s.value = segmentAmount;
        }
    }
    public void UpdateHealthBar(int damage, bool isLethal)
    {
        if(!isInvulnerable)
        {
            if(isLethal)
            {
                foreach(Slider s in sliders)
                {
                    s.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = lethalColor;
                    damageText.color = lethalColor;
                }
            }
            else
            {
                foreach (Slider s in sliders)
                {
                    s.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = nonLethalColor;
                    damageText.color = nonLethalColor;
                }
            }

            if (damage > sliders[segmentNdx].value)
            {
                sliders[segmentNdx].value = 0;
                segmentNdx++;
                if (LayerMask.LayerToName(transform.root.gameObject.layer) == "Boss")
                {
                    StartCoroutine(GoInvulnerable());
                }
            }
            else
            {
                sliders[segmentNdx].value -= damage;
                if (sliders[segmentNdx].value == 0)
                {
                    segmentNdx++;
                    if (LayerMask.LayerToName(transform.root.gameObject.layer) == "Boss")
                    {
                        StartCoroutine(GoInvulnerable());
                    }
                }
            }

            foreach (Slider s in sliders)
            {
                currHealthSum += (int)s.value;
            }

            damageText.text = currHealthSum.ToString();

            if(currHealthSum == 0)
            {
                TimeScaler.instance.PorqinsDead(isLethal);
            }
            currHealthSum = 0;
        }
        
    }

    IEnumerator GoInvulnerable()
    {
        isInvulnerable = true;
        
        foreach (Slider s in sliders)
        {
            s.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = inVulnColor;
            damageText.color = inVulnColor;
        }

        PorqinsBossFight.instance.StartPhase();

        yield return new WaitForSeconds(8f);

        foreach (Slider s in sliders)
        {
            s.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
            damageText.color = lethalColor;
        }
        
        isInvulnerable = false;

    }
}
