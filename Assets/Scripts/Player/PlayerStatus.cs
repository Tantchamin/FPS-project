using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("Status")]
    public int maxHealth = 10;
    public int currentHealth = 10;
    public HealthBar heatlhBar;
    private bool isInvisible = false;
    private int invisibleTime = 2;

    [Header("Guns")]
    public List<Gun> gunInventory;

    public int equipedGun = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack") && !isInvisible)
        {
            EnemyAttack enemyAttack = other.GetComponent<EnemyAttack>();
            GetDamage(enemyAttack.enemy.attackDamage);
            heatlhBar.SetHealth();
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
