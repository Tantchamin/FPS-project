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
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    SoundManager soundManager;
    private float countDown = 1;
    int fastestTime = 5;
    int slowestTime = 15;

    void Start()
    {
        soundManager = SoundManager.GetInstance();
        enemyAnimator = GetComponent<Animator>();
        isIdle = true;
        enemyAnimator.SetBool("isIdle", isIdle);
        countDown = Random.Range(fastestTime, slowestTime);
    }

    private void Update()
    {
        if (isWalking)
        {
            if (!walkAudioSource.isPlaying)
            {
                soundManager.PlaySound("ZombieWalk", true, walkAudioSource);
            }
        }
        else
        {
            if (walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop();
            }
        }

        if(countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        else
        {
            RandomZombieAmbient();
            countDown = Random.Range(fastestTime, slowestTime);
        }
    }

    public void getDamage(int damage)
    {
        if (isDeath) return;
        healtPoint -= damage;
        CheckIsDeath();
    }

    void CheckIsDeath()
    {
        if(healtPoint <= 0)
        {
            isDeath = true;
            isWalking = false;
            soundManager.PlaySound("ZombieDead", false, sfxAudioSource);
            enemyAnimator.SetBool("isDeath", isDeath);
            gameManager.enemyList.Remove(this);
            gameManager.enemyLeft -= 1;
            gameManager.playerUi.SetZombieLeftText();

            if(gameManager.enemyLeft <= 0)
            {
                gameManager.SetState(3);
            }

            Destroy(this.gameObject, 1.1f);
        }
        else
        {
            soundManager.PlaySound("ZombieHitted", false, sfxAudioSource);
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
        soundManager.PlaySound("ZombieAttack", false, sfxAudioSource);
        attackBox.gameObject.SetActive(true);
        enemyAnimator.SetTrigger("attack");
        Invoke(nameof(CloseAttackHitBox), 0.2f);
    }

    private void CloseAttackHitBox()
    {
        attackBox.gameObject.SetActive(false);
    }

    private void RandomZombieAmbient()
    {
        int randomSound = Random.Range(1, 4);
        string soundName = $"ZombieIdle{randomSound}";
        soundManager.PlaySound(soundName, false, sfxAudioSource, 0.1f);
    }

}
