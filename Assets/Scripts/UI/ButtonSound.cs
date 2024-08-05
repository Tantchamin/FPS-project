using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.GetInstance();
    }

    public void PlayButtonSound()
    {
        soundManager.PlayButton("Button");
    }
}
