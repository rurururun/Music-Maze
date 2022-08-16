using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVolume : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource bgm;

    void Start()
    {
        volumeSlider.value = bgm.volume;
    }

    void Update()
    {
        bgm.volume = volumeSlider.value;
    }
}
