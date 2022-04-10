using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] ScoreBaseScriptable scoreHolder;
    [SerializeField] TMP_Text scoreText;

    private void Start()
    {
        
        scoreText.text = scoreHolder.bestScore.ToString();
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
