using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameBase : MonoBehaviour
{

    int score = 0;
    bool win = false;

    public event Action<Vector3> death;
    [SerializeField] GameObject cam;
    [SerializeField] TextMeshProUGUI scoreText, bestScore, yourScore, finaleMessage;
    [SerializeField] GameObject endPanel;
    [SerializeField] ScoreBaseScriptable scoreContainer;

    private void Start()
    {
        scoreText.text = score.ToString();
    }
    public void CallDeath(Vector3 player)
    {
        death(player);
        win = false;
    }
    private void OnEnable()
    {
        death += EndGame;
    }


    public void AddScore(int scoreAdd)
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies.Length);
        score += scoreAdd;
        scoreText.text = score.ToString();
        if (enemies.Length == 1) { win = true; EndGame(GameObject.FindGameObjectWithTag("Player").transform.position); }

    }

    void EndGame(Vector3 player)
    {
        if (win)
        {
            finaleMessage.text = "All Enemies are Dead";
        }
        else finaleMessage.text = "You're Dead";

        scoreContainer.scoreList.Add(scoreContainer.scoreList.Count, score);
        Instantiate(cam, player, Quaternion.identity);
        death -= EndGame;
        WaitAndLoad();

    }

    void WaitAndLoad()
    {
        endPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        foreach (int i in scoreContainer.scoreList.Values)
        {
            if (scoreContainer.bestScore < i) scoreContainer.bestScore = i;
        }
        yourScore.text = score.ToString();
        bestScore.text = scoreContainer.bestScore.ToString();
    } 

    public void ReLoadScene()
    {
        SceneManager.LoadScene(1);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}



