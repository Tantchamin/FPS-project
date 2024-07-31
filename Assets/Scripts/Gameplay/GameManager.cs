using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    public List<Enemy> enemyList;

    private void Awake()
    {
        enemySpawner.gameManager = this;
    }

}
