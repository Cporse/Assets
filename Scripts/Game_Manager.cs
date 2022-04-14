using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    private int score = 0, highScore = 0;

    public int Score { get => score; set => score = value; }
    public int HighScore { get => highScore; set => highScore = value; }

    private void Awake()
    {
        HighScore = PlayerPrefs.GetInt(Statics.HIGH_SCORE);

        if (Instance == null)
            Instance = this;
    }
    public void Auto_Save()
    {
        PlayerPrefs.SetInt(Statics.SCORE, score);
        
        if (score > highScore)
        {
            PlayerPrefs.SetInt(Statics.HIGH_SCORE, score);

        }
    }
}