using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] EnemyFighter boss;
    [SerializeField] Image image;
    int startHealth;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = boss.GetBossHealth();
    }

    // Update is called once per frame
    void Update()
    {
            
        image.fillAmount = (float)boss.GetBossHealth() / startHealth;
    }
}
