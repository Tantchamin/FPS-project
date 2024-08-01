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
    public CameraController cameraController;
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

    private static GameManager gameManager;

    public static GameManager GetInstance()
    {
        return gameManager;
    }


    private void Awake()
    {
        GameManager.gameManager = this;
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
        else
        {
            SetState(1);
            startPanel.SetActive(false);
            SetPlayerControl(true);
        }

        if(enemyLeft > maxZombieInField && enemyList.Count < maxZombieInField)
        {
            enemySpawner.SpawnEnemy();
        }
        
    }

    private void SetPlayerControl(bool isControl)
    {
        playerController.enabled = isControl;
        cameraController.enabled = isControl;
    }

    public void SetState(int newState)
    {
        state = (GameState)newState;
        switch (state)
        {
            case (GameState)0:
                isPause = true;
                // Time.timeScale = 0;
                break;
            case (GameState)1:
                isPause = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                break;
            case (GameState)2:
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                break;
            case (GameState)3:
                isPause = true;
                endPanel.SetActive(true);
                break;
        }
    }

    public void PauseGame(bool pauseState)
    {
        isPause = pauseState;
        int setIndex = pauseState ? 2 : 1;
        SetState(setIndex);
    }

}
