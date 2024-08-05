using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState { 
    start,
    playing,
    pause,
    end,
}

public class GameManager : MonoBehaviour
{
    private GameState state = GameState.start;
    [SerializeField] private EnemySpawner enemySpawner;
    public PlayerStatus playerStatus;
    public PlayerController playerController;
    public CharacterController characterController;
    public CameraController cameraController;
    public Rigidbody playerRigidbody;
    public PlayerUI playerUi;
    public List<Enemy> enemyList;
    public bool isPause = false;
    public bool isEnd = false;
    public GameObject startPanel;
    public TMP_Text countDownText;
    public GameObject pausePanel;
    public GameObject endPanel;
    private float countDownTime = 3;
    public int enemyAmount = 70;
    public int enemyLeft = 70;
    public int maxZombieInField = 40;
    SoundManager soundManager;

    private static GameManager gameManager;

    public static GameManager GetInstance()
    {
        return gameManager;
    }


    private void Awake()
    {
        GameManager.gameManager = this;
        
    }

    private void Start()
    {
        
        soundManager = SoundManager.GetInstance();
        SetCameraCursor(false);
        SetState(0);
        SetPlayerControl(false);
        pausePanel.SetActive(false);
        endPanel.SetActive(false);

    }

    private void Update()
    {
        if(countDownTime > 0 && state == 0)
        {
            if (countDownText.text != Mathf.Round(countDownTime).ToString())
            {
                countDownText.text = $"{Mathf.Round(countDownTime)}";
            }
            countDownTime -= Time.deltaTime;
        }
        
        if(countDownTime <= 0 && state == 0)
        {
            SetState(1);
            startPanel.SetActive(false);
            SetPlayerControl(true);
            soundManager.PlayBgm("GameplayBgm", true);
        }

        if(enemyLeft >= maxZombieInField && enemyList.Count < maxZombieInField)
        {
            enemySpawner.SpawnEnemy();
        }

        if(enemyLeft <= 0 && isEnd)
        {
            SetState(3);
        }
        
    }

    private void SetPlayerControl(bool isControl)
    {
        playerController.enabled = isControl;
        cameraController.enabled = isControl;
        characterController.enabled = isControl;
    }

    public void SetState(int newState)
    {
        state = (GameState)newState;
        switch (state)
        {
            case (GameState)0:
                Time.timeScale = 1;
                isPause = true;
                soundManager.StopBgm();
                break;
            case (GameState)1:
                isPause = false;
                pausePanel.SetActive(false);
                SetCameraCursor(false);
                Time.timeScale = 1;
                break;
            case (GameState)2:
                pausePanel.SetActive(true);
                SetCameraCursor(true);
                Time.timeScale = 0;
                break;
            case (GameState)3:
                isPause = true;
                SetCameraCursor(true);
                bool isSurvive = playerStatus.currentHealth > 0;
                playerUi.SetSurvivedText(isSurvive);
                playerController.enabled = false;
                cameraController.enabled = false;
                endPanel.SetActive(true);
                isEnd = true;
                break;
        }
    }

    void SetCameraCursor(bool isShow)
    {
        Cursor.lockState = isShow ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isShow;
    }

    public void PauseGame(bool pauseState)
    {
        if (state == 0) return;
        isPause = pauseState;
        int setIndex = pauseState ? 2 : 1;
        SetState(setIndex);
    }

}
