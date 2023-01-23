using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject menu;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private GameObject game;

    private float timeRemaining = 20f;
    private bool startGame = false;

    public static MenuManager Instance;

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
        loadScore();
    }

    void loadGameTime()
    {
        timeRemaining = GameManager.Instance.GetGameTime() * 60;
    }

    void PlayGame()
    {
        menu.SetActive(false);
        gameMenu.SetActive(true);
        game.GetComponent<Game>().newGame();
        GameManager.Instance.RestartPlayer();
        startGame = true;
        loadGameTime();
        StartCoroutine(GameCoroutine());
        
    }

    public void EndGame()
    {
        menu.SetActive(true);
        gameMenu.SetActive(false);
        game.GetComponent<Game>().endGame();
        GameManager.Instance.SaveScore();
        loadScore();
        startGame = false;
    }

    public void loadScore()
    {
        bestScoreText.text = "Najlepszy wynik: " + GameManager.Instance.GetBestScore();
        lastScoreText.text = "Ostatni wynik: " + GameManager.Instance.GetLastScore();
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = "Czas: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator GameCoroutine()
    {
        while (startGame)
        {
            scoreText.text = "Punkty: " + GameManager.Instance.GetScore();
            lifeText.text = "¯ycie: " + GameManager.Instance.GetLife();

            yield return new WaitForSeconds(0.1f);
        }
    }

    void timeUpdate()
    {
        if (startGame)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                EndGame();
            }
        }
    }

    void Update()
    {
        if (!startGame && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            PlayGame();
        }
        timeUpdate();
    }


}
