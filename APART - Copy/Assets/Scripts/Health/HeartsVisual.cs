using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class HeartsVisual : MonoBehaviour
{
    public static HeartsHealthSystem heartsHealthSystemStatic;

    [SerializeField] private Sprite heart0Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private Sprite heart2Sprite;
    [SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart4Sprite;
    [SerializeField] private AnimationClip heartFullAnimationClip;

    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;
    private bool isHealing;

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }

    void Start()
    {
        FunctionPeriodic.Create(HealingAnimatedPeriodic, .05f);
        HeartsHealthSystem heartsHealthSystem = new HeartsHealthSystem(5);
        SetHeartsHealthSystem(heartsHealthSystem);
    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        this.heartsHealthSystem = heartsHealthSystem;
        heartsHealthSystemStatic = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        int row = 0;
        int col = 0;
        int colMax = 5;
        float rowColSize = 40f;

        for (int i = 0; i < heartList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartList[i];
            Vector2 heartAnchoredPos = new Vector2(col * rowColSize, -row * rowColSize);
            CreateHeartImage(heartAnchoredPos).SetHeartFragments(heart.GetFragmentAmount());

            col++;
            if (col >= colMax)
            {
                row++;
                col = 0;
            }
        }

        heartsHealthSystem.onDamaged += HeartsHealthSystem_onDamaged;
        heartsHealthSystem.onHealed += HeartsHealthSystem_onHealed;
        heartsHealthSystem.onDead += HeartsHealthSystem_onDead;
    }

    private void HeartsHealthSystem_onDead(object sender, System.EventArgs e)
    {
        //TimeScaler.instance.PlayerDead();
    }

    private void HeartsHealthSystem_onHealed(object sender, System.EventArgs e)
    {
        isHealing = true;

        RefreshAllHearts();
    }

    private void HeartsHealthSystem_onDamaged(object sender, System.EventArgs e)
    {
        RefreshAllHearts();
    }

    private void RefreshAllHearts()
    {
        //Debug.Log("Trying to Refresh Hearts");
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        //Debug.Log(heartImageList.Count);
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartsHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());
            //Debug.Log(i);
        }
    }

    private void HealingAnimatedPeriodic()
    {
        if (isHealing)
        {
            bool fullyHealed = true;
            List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
            for (int i = 0; i < heartList.Count; i++)
            {
                HeartImage heartImage = heartImageList[i];
                HeartsHealthSystem.Heart heart = heartList[i];
                if (heartImage.GetFragmentAmount() != heart.GetFragmentAmount())
                {
                    heartImage.AddHeartVisualFragment();
                }
                fullyHealed = false;
                break;
            }
            if (fullyHealed)
            {
                isHealing = false;
            }
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchoredPos)
    {
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));

        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;
        heartGameObject.transform.localScale = Vector3.one;

        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);

        heartGameObject.GetComponent<Animation>().AddClip(heartFullAnimationClip, "HeartFull");

        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart4Sprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI, heartGameObject.GetComponent<Animation>());
        heartImageList.Add(heartImage);

        return heartImage;
    }

    public class HeartImage
    {
        private int fragments;
        private Image heartImage;
        private HeartsVisual heartsHealthVisual;
        private Animation animation;

        public HeartImage(HeartsVisual heartsHealthVisual, Image heartImage, Animation animation)
        {
            this.heartsHealthVisual = heartsHealthVisual;
            this.heartImage = heartImage;
            this.animation = animation;
        }

        public void SetHeartFragments(int fragments)
        {
            this.fragments = fragments;
            switch (fragments)
            {
                case 0: heartImage.sprite = heartsHealthVisual.heart0Sprite; break;
                    Debug.Log("Passed Case 0");
                case 1: heartImage.sprite = heartsHealthVisual.heart1Sprite; break;
                    Debug.Log("Passed Case 1");
                case 2: heartImage.sprite = heartsHealthVisual.heart2Sprite; break;
                    Debug.Log("Passed Case 2");
                case 3: heartImage.sprite = heartsHealthVisual.heart3Sprite; break;
                    Debug.Log("Passed Case 3");
                case 4: heartImage.sprite = heartsHealthVisual.heart4Sprite; break;
                    Debug.Log("Passed Case 4");

            }
        }

        public int GetFragmentAmount()
        {
            return fragments;
        }

        public void AddHeartVisualFragment()
        {
            SetHeartFragments(fragments + 1);
        }

        public void PlayHeartFullAnimation()
        {
            animation.Play("HeartFull", PlayMode.StopAll);
        }
    }
}
