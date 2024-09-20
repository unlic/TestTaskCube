using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button start;
    [SerializeField] private Button quit;

    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject menuCanvas;


    private string winTitleText = "You Win";
    private string loseTitleText = "You Lose";

    private string restarText = "Restart";
    private string nextLevelText = "Start next level";

    private int levelToStart = 1;
    private int maxLevel = 12;

    private void Start()
    {
        start.onClick.AddListener(StartNewGame);
        quit.onClick.AddListener(Exit);
    }
    private void StartNewGame()
    {
        gameController.StartLevel(levelToStart);
        gameController.WinAction += Win;
        gameController.LoseAction += Lose;
        menuCanvas.SetActive(false);
    }
    private void Exit()
    {
        Application.Quit();
    }

    private void Win(int level)
    {
        menuCanvas.SetActive(true);
        SetText(level, true);
        levelToStart = level + 1;

        if(level > maxLevel)
        {
            level = 1;
        }
    } 

    private void Lose(int level)
    {
        menuCanvas.SetActive(true);
        SetText(level, false);
        levelToStart = 1;
    }

    private void SetText(int level, bool isWin)
    {
        titleText.text = isWin ? winTitleText : loseTitleText;
        levelText.text = $"Level {level}/{maxLevel}";
        buttonText.text = isWin ? nextLevelText : restarText;
    }
}
