using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator enemyAnimator;
    public int healtPoint = 2;
    int attackDamage = 1;
    float moveSpeed = 5;
    bool isDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamage(int damage)
    {
        healtPoint -= damage;
        checkIsDeath();
    }

    void checkIsDeath()
    {
        if(healtPoint <= 0)
        {
            Debug.Log("Death");
            isDeath = true;
            enemyAnimator.SetBool("isDeath", isDeath);
            Destroy(this.gameObject, 1.1f);
        }
    }

}
