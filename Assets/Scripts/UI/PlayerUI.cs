using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text ammoText;
    public TMP_Text zombieLeftText;
    public TMP_Text surviveText;
    public PlayerStatus playerStatus;
    Gun equppingGun;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        SetAmmoText();
        SetZombieLeftText();
    }

    public void SetAmmoText()
    {
        equppingGun = playerStatus.gunInventory[playerStatus.equipedGun];
        ammoText.text = $"Ammo : {equppingGun.currentAmmo} / {equppingGun.maxAmmo}";
    }

    public void SetZombieLeftText()
    {
        zombieLeftText.text = $"Zombie left : {gameManager.enemyLeft}";
    }

    public void SetSurvivedText(bool isSurvive)
    {
        surviveText.text = isSurvive ? "Suvived" : "You are dead...";
    }

}
