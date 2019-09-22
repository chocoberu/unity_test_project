using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public enum State // 적 캐릭터의 상태를 표현하기 위한 열거형 변수
    {
        TRACE,
        ATTACK,
        DIE
    }
    public State state = State.TRACE;
    private Transform playerTr; // 플레이어의 위치
    private Transform enemyTr; // 적의 위치
    public float attackDist = 1.0f;

    private WaitForSeconds ws;
    private EnemyMoveAgent moveAgent; // 이동을 제어하는 EnemyMoveAgent 클래스를 저정할 변수

    public int enemyHP = 1;
    private Animator animator;
    private Rigidbody rigidbody;
    private BoxCollider boxCollider;
    public GameObject swordObject;
    private Sword sword;
    public bool isDead = false;
    private WaitForSeconds wsHit;

    
    // Start is called before the first frame update
    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            playerTr = player.GetComponent<Transform>();
        }
        
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        moveAgent = GetComponent<EnemyMoveAgent>();
        sword = swordObject.GetComponent<Sword>();
        
        ws = new WaitForSeconds(0.3f);
        wsHit = new WaitForSeconds(1.05f);
    }
    void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            if (state == State.DIE) // 사망했다면 코루틴 함수 종료
                yield break;
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            if(dist <= attackDist) // 공격 범위 이내에 있다면 공격
            {
                state = State.ATTACK;
            }
            else // 공격 범위 이외에 있다면 추적
            {
                state = State.TRACE;
            }

            yield return ws; // 0.3초 동안 대기하는 동안 제어권을 양보
        }
    }

    IEnumerator Action()
    {
        while(!isDead)
        {
            yield return ws;
            switch(state)
            {
                case State.TRACE:
                    moveAgent.traceTarget = playerTr.position;
                    animator.SetBool("isMove", true);
                    break;
                case State.ATTACK:
                    moveAgent.Stop();
                    sword.SwordAttack(wsHit);
                    animator.SetTrigger("isAttack");
                    break;
                case State.DIE:
                    moveAgent.Stop();
                    if(isDead == false)
                    {
                        isDead = true;
                        EnemyDead();
                    }
                    break;
            }
        }
                
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHP <= 0)
        {
            if (isDead == false)
            {
                state = State.DIE;
                isDead = true;
                EnemyDead();
            }
        }
    }
    void EnemyDead()
    {
        swordObject.GetComponent<BoxCollider>().enabled = false;
        moveAgent.Stop();
        animator.SetTrigger("Die");
        Destroy(this.gameObject, 2.0f);
        boxCollider.enabled = false;
        rigidbody.isKinematic = true;
    }
    public void Damaged()
    {
        enemyHP--;
    }
}
