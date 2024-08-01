using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text ammoText;
    public TMP_Text zombieLeftText;
    public PlayerStatus playerStatus;
    Gun equppingGun;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        SetAmmoText();
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

}
