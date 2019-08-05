using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCtrl : MonoBehaviour
{

    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;

    private Transform tr; // 접근해야 하는 컴포넌트는 반드시 변수에 할당한 후 사용
    public float moveSpeed = 10.0f; // 이동속도
    public float rotSpeed = 80.0f; // 회전속도
    public float jumpPower = 5.0f; // 점프 파워
    private Animator animator; // 애니메이터 컴포넌트를 추출하고 저장하는 변수
    private Rigidbody rigidbody; // rigidbody 컴포넌트를 추출하고 저장하는 변수

    bool isAttack;
    bool isJump;

    Vector3 moveDir;
    WaitForSeconds wsHit;

    // Start is called before the first frame update
    void Awake()
    {
        tr = GetComponent<Transform>(); // tr에 트랜스폼 컴포넌트 할당
        animator = GetComponent<Animator>(); // animator에 Animator 컴포넌트 할당
        rigidbody = GetComponent<Rigidbody>();
        wsHit = new WaitForSeconds(0.4f);

        isAttack = isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        //r = Input.GetAxis("Mouse X");

        //Debug.Log("h = " + h.ToString());
        //Debug.Log("v = " + v.ToString());

        if (Input.GetKeyDown(KeyCode.Z))
        {
            isAttack = true;             
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            isJump = true;
        }
        
    }
    void FixedUpdate()
    {
        Run(h, v);
        Turn();
        Hit();
        Jump();
    }
    void Run(float h, float v)
    {
        //moveDir = (Vector3.forward * v) + (Vector3.right * h); // 전후좌우 이동 방향 벡터 계산
        moveDir.Set(h, 0, v);
        //Translate(이동방향 * 속도 * 변위값 * Time.deltaTime, 기준좌표)
        //tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime,Space.World); //self는 로컬좌표
        moveDir = moveDir.normalized * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(tr.position + moveDir);
        

        // 애니메이션 수행
        if (h == 0 && v == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }
    
    void Turn()
    {
        Quaternion rot = Quaternion.LookRotation(moveDir);
        rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, rot, rotSpeed * Time.deltaTime);
    }

   
    void Hit()
    {
        if (isAttack)
        {
            animator.SetTrigger("isAttack");
            isAttack = false;
        }
    }

    void Jump()
    {
        if(isJump)
        {
            animator.SetTrigger("isJump");
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = false;
        }
    }
}
