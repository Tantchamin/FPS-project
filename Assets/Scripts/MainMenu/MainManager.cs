using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPage;
    [SerializeField] private GameObject creditPage;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.GetInstance();

        const int startTime = 5;
        const int repeatTime = 12;
        InvokeRepeating(nameof(RandomZombieAmbient), startTime, repeatTime);
    }

    public void TutorialButton()
    {
        bool isActive = tutorialPage.activeSelf;
        tutorialPage.SetActive(!isActive);
    }

    public void CreditButton()
    {
        bool isActive = creditPage.activeSelf;
        creditPage.SetActive(!isActive);
    }

    private void RandomZombieAmbient()
    {
        int randomSound = Random.Range(1, 4);
        string soundName = $"ZombieIdle{randomSound}";
        soundManager.PlaySound(soundName, false, null, 1f);
    }
}
