using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene_Manager : MonoBehaviour
{
    [SerializeField] private Text highScore;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void BestScore()
    {
        int HighScore = PlayerPrefs.GetInt(Statics.HIGH_SCORE);
        highScore.text = "BEST Score : " + HighScore.ToString();
    }
}