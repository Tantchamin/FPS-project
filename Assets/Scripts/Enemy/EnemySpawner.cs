using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemy;
    [SerializeField] private PlayerStatus player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedEnemy = Instantiate(enemy[0].gameObject, new Vector3(0, 0, 2), this.transform.rotation);
        spawnedEnemy.GetComponent<EnemyAi>().player = player.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
