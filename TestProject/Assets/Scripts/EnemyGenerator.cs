using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public List<Transform> genPoint;
    public GameObject Enemy;
    private WaitForSeconds ws;
    private PlayerCtrl pCtrl;
    // Start is called before the first frame update
    void Start()
    {
        ws = new WaitForSeconds(10.0f);
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            pCtrl = player.GetComponent<PlayerCtrl>();

        StartCoroutine(GenEnemy(ws));
    }

    IEnumerator GenEnemy(WaitForSeconds ws)
    {
        while(true)
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(Enemy, genPoint[i]);
            }
            if (pCtrl.isDead)
            {
                break;
            }
            yield return ws;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
