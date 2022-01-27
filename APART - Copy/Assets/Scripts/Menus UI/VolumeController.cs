using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioSource audioS;

    public void ChangeAudio()
    {
        audioS.volume = (GetComponent<SliderValue>().GetSlideValue() / 100);
    }
}
