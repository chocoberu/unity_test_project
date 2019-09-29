using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public List<Transform> genPoint;
    public GameObject Enemy;
    private WaitForSeconds ws;
    private PlayerCtrl pCtrl;
    private int enemyGenCnt = 4;
    // Start is called before the first frame update
    void Start()
    {
        ws = new WaitForSeconds(7.0f);
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            pCtrl = player.GetComponent<PlayerCtrl>();

        StartCoroutine(GenEnemy(ws));
    }

    IEnumerator GenEnemy(WaitForSeconds ws)
    {
        while(pCtrl.playerHP > 0)
        {
            for (int i = 0; i < enemyGenCnt; i++)
            {
                Instantiate(Enemy, genPoint[Random.Range(0,4)]);
            }
            if (pCtrl.isDead)
            {
                break;
            }
            yield return ws;
        }
    }
    public void SetEnemyGenCnt(int cnt)
    {
        enemyGenCnt = cnt;
    }
}
