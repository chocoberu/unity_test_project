using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int enemyHP = 1;
    private Animator animator;
    private Rigidbody rigidbody;
    private BoxCollider boxCollider;
    private bool isDead = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            enemyHP--;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHP <= 0)
        {
            if (isDead == false)
            {
                EnemyDead();
                isDead = true;
            }
        }
    }
    void EnemyDead()
    {
        animator.SetTrigger("Die");
        Destroy(this.gameObject, 2.0f);
        boxCollider.enabled = false;
    }
}
