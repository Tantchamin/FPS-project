using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;
    Animator enemyAnimator;
    public int healtPoint = 2;
    public int attackDamage = 1;
    public float moveSpeed = 7;
    public bool isDeath = false;
    public bool isIdle = false;
    public bool isWalking = false;
    public float timeBeforeAttack = 0.5f;
    public float timeBetweenAttack = 1f;
    [SerializeField] private BoxCollider attackBox;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        isIdle = true;
        enemyAnimator.SetBool("isIdle", isIdle);
    }

    public void getDamage(int damage)
    {
        healtPoint -= damage;
        CheckIsDeath();
    }

    void CheckIsDeath()
    {
        if(healtPoint <= 0)
        {
            Debug.Log("Death");
            isDeath = true;
            enemyAnimator.SetBool("isDeath", isDeath);
            gameManager.enemyList.Remove(this);
            gameManager.enemyLeft -= 1;
            gameManager.playerUi.SetZombieLeftText();
            Destroy(this.gameObject, 1.1f);
        }
        else
        {
            enemyAnimator.SetTrigger("getDamage");
        }
    }

    public void Walking()
    {
        isWalking = true;
        isIdle = false;
        enemyAnimator.SetBool("isWalking", isWalking);
        enemyAnimator.SetBool("isIdle", isIdle);
    }

    public void Attack()
    {
        isWalking = false;
        attackBox.gameObject.SetActive(true);
        enemyAnimator.SetTrigger("attack");
        Invoke(nameof(CloseAttackHitBox), 0.2f);
    }

    private void CloseAttackHitBox()
    {
        attackBox.gameObject.SetActive(false);
    }

}
