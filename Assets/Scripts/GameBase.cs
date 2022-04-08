using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameBase : MonoBehaviour
{

    int score = 0;

    public event Action<Vector3> death;
    [SerializeField] GameObject cam;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] ScoreBaseScriptable scoreContainer;

    private void Start()
    {
        scoreText.text = score.ToString();
    }
    public void CallDeath(Vector3 player)
    {
        death(player);
    }
    private void OnEnable()
    {
        death += EndGame;
    }

    public void AddScore(int scoreAdd)
    {
        score += scoreAdd;
        scoreText.text = score.ToString();
    }

    void EndGame(Vector3 player)
    {
        scoreContainer.scoreList.Add(score, "me");
        Instantiate(cam, player, Quaternion.identity);
        death -= EndGame;
        StartCoroutine(WaitAndLoad());
        
    }

    IEnumerator WaitAndLoad()
    {
       
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
        
    } 
}



