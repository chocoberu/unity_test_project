using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;
    private int level = 1;
    public Text scoreText;

    public EnemyGenerator enemyGenerator;

    public int GetScore()
    {
        return score;
    }
    public void SetScore(int point)
    {
        score += point;
        SetScoreText();

        if(score > 100 && level == 1)
        {
            enemyGenerator.SetEnemyGenCnt(6);
            level++;
        }
        if(score > 200 && level == 2)
        {
            enemyGenerator.SetEnemyGenCnt(8);
            level++;
        }
        if (score > 400 && level == 3)
        {
            enemyGenerator.SetEnemyGenCnt(12);
            level++;
        }
        if (score > 600 && level == 4)
        {
            enemyGenerator.SetEnemyGenCnt(16);
            level++;
        }
        if (score > 800 && level == 5)
        {
            enemyGenerator.SetEnemyGenCnt(20);
            level++;
        }
    }
    void SetScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
    }
}
