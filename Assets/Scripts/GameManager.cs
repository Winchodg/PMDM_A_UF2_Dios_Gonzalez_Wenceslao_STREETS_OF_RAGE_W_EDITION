using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    const int LIVES = 3;

    const int LIFE_UP = 300;

    const int HEALTH = 100;

    const int ENEMY_SCORE = 50;

    const int BARREL_SCORE = 10;
    const string DATA_FILE = "data.json";

    static GameManager instance;

    public static bool isDead = false;

    [SerializeField] Text txtScore;
    [SerializeField] Text txtHScore;
    [SerializeField] Text txtMessage;
    [SerializeField] Text txtHealth;
    //[SerializeField] Image[] imgLives;
    [SerializeField] AudioClip sfxLifeUp;
    [SerializeField] AudioClip sfxGameOver;
    [SerializeField] AudioClip sfxGameOver2;
    [SerializeField] GameObject playerGiveUp;
    

    int score;
    //int lives = LIVES;
    int startHealth = 100;
    int health = HEALTH;

    bool extraLife = false; 
    bool paused = false;
    bool gameOver = false;

    GameData gameData;

    GameObject car, player;

    public static GameManager GetInstance()
    {
        return instance;
    }

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy (gameObject);
        }
    
    }

    void Start()
    {
        Cursor.visible = false;

        gameData = LoadData();

        car = GameObject.FindWithTag("Car");
        player = GameObject.FindWithTag("Player");
    }

    GameData LoadData()
    {
        if (File.Exists(DATA_FILE))
        {
            string fileText = File.ReadAllText(DATA_FILE);

            return JsonUtility.FromJson<GameData>(fileText);
        }
        else
            return new GameData();
    }

    void SaveData()
    {
        // creamos la representación JSON de gamedata.
        string json = JsonUtility.ToJson(gameData);

        // volcar sobre archivo
        File.WriteAllText(DATA_FILE, json);
    }

    // sumar puntos.
    public void AddScore (string tag)
    {
        int pts = 0;

        switch(tag)
        {
            case "Enemy":
                pts = ENEMY_SCORE;
                break;

            case "Barrel":
                pts = BARREL_SCORE;
                break;
        }

        score += pts;


        // chequeamos si hay vida extra.
        if (score > LIFE_UP && !extraLife)
        {
            LifeUp();
        }

        // actualizar hscore
        if (score > gameData.hscore)
            gameData.hscore = score;
    }

    private void OnGUI()
    {
        // activar iconos de las vidas.
        /*for (int i=0; i<imgLives.Length; i++)
        {
            imgLives[i].enabled = i < lives-1;
        }*/

        // mostrar puntuación de la partida actual.
        txtScore.text = string.Format("{0,4:D4}", score);

        // mostrar puntuación máxima.
        txtHScore.text = string.Format("{0,4:D4}", gameData.hscore);

        // mostrar cantidad de vida restante.
        txtHealth.text = string.Format("{0,3:D3}", health);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
        if (gameOver && Input.GetKeyDown(KeyCode.Return))
            RestartGame();
    }

    void LifeUp ()
    {
        startHealth = 150;
        extraLife = true;
        AudioSource.PlayClipAtPoint(sfxLifeUp, Camera.main.transform.position, 1);
    }

    public void HealthManager (int healthAmount)
    {

        health += healthAmount;

        if (health > startHealth ) 
        {
            health = startHealth;
        }
        else if (health <= 0)
        {
            health = 0;
            isDead = true;
            Invoke("GameOver", 0.05f);
        }
    }

    void PauseGame()
    {
        paused = true;

        Camera.main.GetComponent<AudioSource>().Stop();

        txtMessage.text = "PAUSED\nPRESS <P> TO RESUME";

        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        paused = false;

        Camera.main.GetComponent<AudioSource>().Play();

        txtMessage.text = "";

        Time.timeScale = 1;
    }

    void GameOver ()
    {
        gameOver = true;

        Camera.main.GetComponent<AudioSource>().Stop();

        AudioSource.PlayClipAtPoint(sfxGameOver2, Camera.main.transform.position, 1);

        AudioSource.PlayClipAtPoint(sfxGameOver, Camera.main.transform.position, 1);

        Time.timeScale = 0;

        txtMessage.text = "GAME OVER\nPRESS <RET>> TO RESTART";

        //ó rematar a partida invocamos este método para gardar o highScore no ficheiro Json.
        SaveData();

        player.SetActive(false);
        Instantiate(playerGiveUp, player.transform.position, Quaternion.identity);

    }

    void RestartGame()
    {
        gameOver = false;

        if (car != null)
            Destroy(car);

        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

}
