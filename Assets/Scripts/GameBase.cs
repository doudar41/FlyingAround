using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
public class GameBase : MonoBehaviour
{

    int score = 0;
    bool win = false;
    List<GameObject> enemies = new List<GameObject>();


    public event Action<Vector3> deathOfPlayer;


    [SerializeField]bool bossLevel = false;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject Boss, healthBar;
    [SerializeField] TextMeshProUGUI scoreText, bestScore, yourScore, finaleMessage;
    [SerializeField] GameObject endPanel;
    [SerializeField] ScoreBaseScriptable scoreContainer;
    [SerializeField] PlayerInput playerInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        endPanel.SetActive(false);
        playerInput.defaultActionMap = "Player";
        Time.timeScale = 1;

        var en = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject i in en)
        {
            enemies.Add(i);
        }
        
        scoreText.text = score.ToString();
    }


    public void OpenMenu()
    {
        bool pan = endPanel.active;
        endPanel.SetActive(!pan);
        if (!pan) { 
            playerInput.defaultActionMap = "UI"; 
            Cursor.lockState = CursorLockMode.Confined;
            yourScore.text = score.ToString();
            bestScore.text = scoreContainer.bestScore.ToString();
            
        }
        else { 
            playerInput.defaultActionMap = "Player"; 
            Cursor.lockState = CursorLockMode.Locked; 
        }

        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void CallPlayerDeath(Vector3 player)
    {
        deathOfPlayer(player);
        win = false;
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


/*    public void escapeMenu()
    {
        endPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }*/

    public void ReloadScene()
    {
        endPanel.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (SceneManager.sceneCountInBuildSettings < nextScene)
        {
            SceneManager.LoadScene(0);
        }
        else
        SceneManager.LoadScene(nextScene);
    }
    public void EndGame()
    {

    #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    #endif
    #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
    #elif (UNITY_STANDALONE)
        Application.Quit();
    #elif (UNITY_WEBGL)
        Application.OpenURL("about:blank");
    #endif

    }
}



