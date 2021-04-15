using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;//ınput output için
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{
    [Tooltip("Restart delay")]
    public float delay;
    public static GameController instance;   
    public GameData data; 
    public GameObject wall;
    public GameObject enemySpawner;
    public GameObject rewardCoin;
    

    public UI ui;


    private BinaryFormatter binaryFormatter;
    private string filePath;
    private bool paused;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        binaryFormatter = new BinaryFormatter();

        filePath = Application.persistentDataPath + "/game.dat";

    }
    private void Start()
    {
        DataCtrl.instance.LoadData();
        data = DataCtrl.instance.data;
        RefreshUI();
        LoadGame();
        UptadeHearths();

        paused = false;

        //LevelComplete();
    }
    private void Update()
    {
        if(paused)
        {
            Time.timeScale = 0;
        }
        if(!paused)
        {
            Time.timeScale = 1;
        }
    }
    /// <summary>
    /// Player ölünce oyunu restar eder.
    /// </summary>
    public void PlayerDied(GameObject player)
    {
         player.SetActive(false);
         CheckLives();
         //Invoke("RestartLevel", delay);

    }

    public void PlayerHit(GameObject player)
    {
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(-200f, 350f));
        player.transform.Rotate(new Vector3(0, 0, 30f));

        player.GetComponent<PlayerController>().enabled = false; 
        player.GetComponent<Collider2D>().enabled = false; 
       
        foreach(Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }
        rigid.velocity = Vector2.zero; 

        StartCoroutine("GamePause",player);
    }

    IEnumerator GamePause(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        PlayerDied(player);
    }
    public void BulletHit(GameObject enemy)
    {
        EffectController.instance.EnemyDie(enemy);
        AudioController.instance.EnemyDieSound(enemy.transform.position);
        Instantiate(rewardCoin, enemy.transform.position, Quaternion.identity);
        //big bonus
        Destroy(enemy);
        //uptadescore
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void RefreshUI()
    {
        ui.mytext.text = " " + data.coin;
        ui.scoreTxt.text = "Score : " + data.score;
    }
    /*
    private void DeleteData()
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        data.coin = 0;
        data.score = 0;
        ui.mytext.text = "0";
        ui.scoreTxt.text = "Score : " + data.score;
        data.lives = 3;
        for (int i = 0; i < 3; i++)
        {
            data.keyValue[i] = false;
        }

        foreach (LevelData level in data.levelData)
        {
            if(level.levelNum !=1)
            {
                level.unlocked = false;
            }
        }

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();

    }
    */


    private void OnEnable()
    {
        RefreshUI();
    }
    private void OnDisable()
    {
        DataCtrl.instance.SaveData(data);
        Time.timeScale = 1;

        //AdCtrl.instance.HideBanner();//***********************************
    }

    private void LoadGame()
    {
        if(data.firstLoading)
        {
            data.lives = 3;
            data.coin = 0;
            data.score = 0;
            data.firstLoading = false;

            for (int i = 0; i < 3; i++)
            {
                data.keyValue[i] = false;
            }
        }
    }

    public void CoinCount()
    {
        data.coin += 1;
        ui.mytext.text = " " + data.coin;
        
    }
    public void ScoreCount(int val)
    {
        data.score += val;
        ui.scoreTxt.text = "Score : " + data.score;
    }
    public void KeyCount(int key)
    {
        data.keyValue[key] = true;

        if (key == 0)
            ui.blue.sprite = ui.blueFull;
        else if (key == 1)
            ui.green.sprite = ui.greenFull;
        else if (key == 2)
            ui.yellow.sprite = ui.yellowFull;
    }
    private void UptadeHearths()
    {
        if(data.lives == 3)
        {
            ui.hearth1.sprite = ui.fullHearth;
            ui.hearth2.sprite = ui.fullHearth;
            ui.hearth3.sprite = ui.fullHearth;
        }

        if (data.lives == 2)
        {
            ui.hearth1.sprite = ui.emptyHearth;
        }
        if (data.lives == 1)
        {
            ui.hearth1.sprite = ui.emptyHearth;
            ui.hearth2.sprite = ui.emptyHearth;
        }
    }
    private void CheckLives()
    {
        
        int currentLives = data.lives;
        currentLives -= 1;
        data.lives = currentLives;

        if (data.lives == 0)
        {
            data.lives = 3;
            DataCtrl.instance.SaveData(data);
            Invoke("GameOver", delay);
        }
        else
        {
            DataCtrl.instance.SaveData(data);
            Invoke("RestartLevel", delay);
        }
    }
    private void GameOver()
    {
        ui.gameOverPanel.SetActive(true);
        ui.mobileUI.SetActive(false);
    }
    public void StopCamera()
    {
        Camera.main.GetComponent<CameraController>().enabled = false;
    }
    public void DisableWall()
    {
        wall.SetActive(false);
        EffectController.instance.ShowPowerUpEffect(wall.transform.position);
        AudioController.instance.EnemyDieSound(wall.transform.position);
        DisableEnemySpawner();

        Invoke("LevelComplete", 3f);
    }

    public void LevelComplete()
    {
        ui.mobileUI.SetActive(false);
        ui.levelCompleteUI.SetActive(true);
    }
    public void EnableSpawner()
    {
        enemySpawner.SetActive(true);
    }
    public void DisableEnemySpawner()
    {
        enemySpawner.SetActive(false);
    }
    public int GetScore()
    {
        return data.score;
    }
    public void UnlockLevel(int levelNum)
    {
        data.levelData[levelNum].unlocked = true;
    }
    public void ShowPauseMenu()
    {
        if(ui.mobileUI.activeInHierarchy)
        {
            ui.mobileUI.SetActive(false);
        }
        
        ui.pauseUI.SetActive(true);
        paused = true;

    }
    public void HidePauseMenu()
    {
        if(!ui.mobileUI.activeInHierarchy)
        {
            ui.mobileUI.SetActive(true);
        }
        ui.pauseUI.SetActive(false);
        paused = false;

    }
}
[System.Serializable]
public class GameData
{
    public int coin;
    public int score;
    public int lives;
    public bool firstLoading;
    public bool[] keyValue;
    public LevelData[] levelData;
    public bool playMusic;
}
[System.Serializable] 
public class UI
{
    [Header("Text özellikler")]
    public Text mytext; //Coin Text
    public Text scoreTxt; //Score

    [Header("Image özellikleri")]
    public Image blue;
    public Image green;
    public Image yellow;

    public Sprite blueFull;
    public Sprite greenFull;
    public Sprite yellowFull;

    public Image hearth1;
    public Image hearth2;
    public Image hearth3;
    public Sprite emptyHearth;
    public Sprite fullHearth;

    [Header("Oyun sonu ekranı")]
    public GameObject gameOverPanel;
    public GameObject levelCompleteUI;
    public GameObject mobileUI;
    public GameObject pauseUI;

}

[System.Serializable]
public class LevelData
{
    public bool unlocked;
    public int levelNum;
}
