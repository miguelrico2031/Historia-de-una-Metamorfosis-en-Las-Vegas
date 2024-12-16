using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        ulong score;
        var scoreStr = PlayerPrefs.GetString("Score", "0");
        if (!ulong.TryParse(scoreStr, out score)) Debug.LogError("jodienda parseando score");

        ulong hiScore;
        var hiScoreStr = PlayerPrefs.GetString("HiScore", "0");
        if (!ulong.TryParse(hiScoreStr, out hiScore)) Debug.LogError("jodienda parseando hiscore");

        if (score >= hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetString("HiScore", hiScore.ToString());
        }

        _scoreText.text = $"Dinero ganado: {FormatMoney(score)}\nMejor partida: {FormatMoney(hiScore)}";
    }

    private string FormatMoney(ulong money)
    {
        string paddedMoney = money.ToString($"D18");
        for (int i = paddedMoney.Length - 3; i > 0; i -= 3)
        {
            paddedMoney = paddedMoney.Insert(i, " ");
        }

        return paddedMoney;
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}