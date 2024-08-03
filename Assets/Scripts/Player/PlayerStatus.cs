using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Status")]
    public int maxHealth = 10;
    public int currentHealth = 10;
    public HealthBar heatlhBar;
    private bool isInvisible = false;
    private int invisibleTime = 1;

    [Header("Guns")]
    public List<Gun> gunInventory;

    public int equipedGun = 0;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.GetInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack") && !isInvisible)
        {
            EnemyAttack enemyAttack = other.GetComponent<EnemyAttack>();
            GetDamage(enemyAttack.enemy.attackDamage);
            heatlhBar.SetHealth();
            soundManager.PlaySound("PlayerHitted", false);

            if(currentHealth <= 0)
            {
                GameManager gameManager = GameManager.GetInstance();
                gameManager.SetState(3);
            }
            StartCoroutine(ResetInvisible());
        }
    }

    IEnumerator ResetInvisible()
    {
        isInvisible = true;
        yield return new WaitForSeconds(invisibleTime);
        isInvisible = false;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
    }

}
