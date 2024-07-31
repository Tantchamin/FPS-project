using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private List<Enemy> enemy;
    [SerializeField] private PlayerStatus player;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(enemy[0].gameObject, new Vector3(0, 0, 2), this.transform.rotation);
        spawnedEnemy.GetComponent<EnemyAi>().player = player.gameObject.transform;
        Enemy enemyStatus = spawnedEnemy.GetComponent<Enemy>();
        enemyStatus.gameManager = gameManager;
        gameManager.enemyList.Add(enemyStatus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
