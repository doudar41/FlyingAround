using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class GameBase : MonoBehaviour
{

    int score = 0;
    bool win = false;
    List<GameObject> enemies = new List<GameObject>();


    event Action<Vector3> deathOfPlayer;




    [SerializeField]bool bossLevel = false;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject Boss, healthBar;
    [SerializeField] TextMeshProUGUI scoreText, bestScore, yourScore, finaleMessage;
    [SerializeField] GameObject endPanel;
    [SerializeField] ScoreBaseScriptable scoreContainer;

    private void Start()
    {
        var en = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject i in en)
        {
            enemies.Add(i);
            //Debug.Log(i.name);
        }
        
        scoreText.text = score.ToString();
    }
    public void CallPlayerDeath(Vector3 player)
    {
        deathOfPlayer(player);
        win = false;
    }
    
    public void CallBossDeath()
    {
        
    }

    private void OnEnable()
    {
        deathOfPlayer += EndGame;
    }

    private void OnDisable()
    {
        deathOfPlayer -= EndGame;
    }

    public void AddScore(int scoreAdd, GameObject enemy)
    {
        score += scoreAdd;
        scoreText.text = score.ToString();
        enemies.Remove(enemy);
        Debug.Log(enemies.Count);

        if (enemy.tag == "Boss")
        {
            win = true;
            EndGame(GameObject.FindGameObjectWithTag("Player").transform.position);
            
        }
        if (enemies.Count == 0) 
        {
            if (bossLevel)
            {
                BossFight();
            }
            else
            {
                win = true;
                EndGame(GameObject.FindGameObjectWithTag("Player").transform.position);
                
            }
        }
    }


    void BossFight()
    {
        Boss.SetActive(true);
        healthBar.SetActive(true);
        Boss.GetComponent<PlayableDirector>().Play();
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
        deathOfPlayer -= EndGame;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}



