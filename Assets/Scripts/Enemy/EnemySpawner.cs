using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemy;
    [SerializeField] private PlayerStatus player;
    [SerializeField] private List<Transform> spawnPoints;

    public void SpawnEnemy()
    {
        GameManager gameManager = GameManager.GetInstance();
        int randomIndex = Random.Range(0, spawnPoints.Count);
        GameObject spawnedEnemy = Instantiate(enemy[0].gameObject, spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation);
        spawnedEnemy.GetComponent<EnemyAi>().player = player.gameObject.transform;
        Enemy enemyStatus = spawnedEnemy.GetComponent<Enemy>();
        enemyStatus.gameManager = gameManager;
        gameManager.enemyList.Add(enemyStatus);
    }

}
