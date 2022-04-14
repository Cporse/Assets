using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Canvas_Manager : MonoBehaviour
{
    Game_Manager gameManager;
    public static Canvas_Manager Instance;
    [SerializeField] private Text hitText;
    [SerializeField] private Text GameOverText;
    //[SerializeField] private Text highScore;

    private bool pause = true;

    private void Start()
    {
        GameOverText.gameObject.SetActive(false);
        gameManager = Game_Manager.Instance;
        int highscore = gameManager.HighScore;
        //highScore.text = "BEST : " + highscore;
    }
    private void Awake()    //
    {
        if (Instance == null)
            Instance = this;
    }
    public void UpdateScore()
    {
        gameManager.Score += 1;
        hitText.text = "Hit : " + gameManager.Score.ToString();
        //hitText.text = "Hit : " + (PlayerPrefs.GetInt("score")).ToString();
        //highScore.text = "BEST : " + (PlayerPrefs.GetInt("high_score")).ToString();
    }
    public void GameOver()
    {
        //Time.timeScale = 0;
        gameManager.Auto_Save();
        GameOverText.gameObject.SetActive(true);
        GameOverText.text = "  Game Over :((\nYour are score: " + gameManager.Score;
    }

    public void Pause()
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        pause = !pause;
    }
}