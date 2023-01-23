using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int pointsForDestruction = 10;
    [SerializeField] private int life = 3;
    [SerializeField] private int gameTime = 1;
    private int score = 0;
    private int startLife;
    public int bestScore = 0;
    public int lastScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        startLife = life;
        LoadScore();
    }

    public void AddScore()
    {
        score += pointsForDestruction;
    }

    public int GetScore()
    {
        return score;
    }

    public void lifeLost()
    {
        life--;
        if (life <= 0)
            MenuManager.Instance.EndGame();
    }
    public void RestartPlayer()
    {
        life = startLife;
        score = 0;
    }

    public int GetLife()
    {
        return life;
    }

    public int GetGameTime()
    {
        return gameTime;
    }
    public int GetBestScore()
    {
        return bestScore;
    }
    public int GetLastScore()
    {
        return lastScore;
    }
    public void SaveScore()
    {
        lastScore = score;
        if(score > bestScore)
            bestScore = score;
        SaveSystem.SaveScore();
    }
    public void LoadScore()
    {
        ScoreData data = SaveSystem.LoadScore();
        bestScore = data.bestScore;
        lastScore = data.lastScore;
    }
}
