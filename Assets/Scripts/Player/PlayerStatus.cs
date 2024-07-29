using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private int healthPoint = 10;

    [Header("Guns")]
    public List<Gun> gunInventory;

    public int equipedGun = 0;

}
