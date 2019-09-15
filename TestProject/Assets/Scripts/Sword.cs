using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public readonly int Damage = 1; // 칼의 공격력
    private BoxCollider boxCollider;
    //WaitForSeconds wsHit;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        //wsHit = new WaitForSeconds(0.85f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player 공격 받음");
            other.gameObject.GetComponent<PlayerCtrl>().Damaged();
        }
        if(other.tag == "Enemy")
        {
            Debug.Log("Enemy 공격 받음");
            other.gameObject.GetComponent<EnemyAI>().Damaged();
        }
    }

    public void SwordAttack(WaitForSeconds wsHit)
    {
        boxCollider.enabled = true;
        StartCoroutine(SwordColliderUnEnabled(wsHit));
    }

    IEnumerator SwordColliderUnEnabled(WaitForSeconds wsHit)
    {
        yield return wsHit;
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
