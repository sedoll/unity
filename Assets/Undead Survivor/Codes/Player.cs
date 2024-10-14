using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 3;
    
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    
    // 초기화
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // 처음에 시작할 때 실행하는 함수
    void Start()
    {
        
    }

    // 매 프레임 마다 실행되는 함수
    void Update()
    {
        // GetAxis, 움직임이 좀 더 부드럽고 미끄러운 느낌이 듬
        // inputVec.x = Input.GetAxisRaw("Horizontal"); // 수평값
        // inputVec.y = Input.GetAxisRaw("Vertical"); // 수직값

        // GetAxisRaw, 덜 부드럽고 세밀한 컨트롤이 가능
        inputVec.x = Input.GetAxisRaw("Horizontal"); // 수평값
        inputVec.y = Input.GetAxisRaw("Vertical"); // 수직값
    }

    // 물리 연산 프레임마다 호출되는 생명주기 함수
    private void FixedUpdate()
    {
        #region 움직이는 방법 세가지
        // 1. 힘을 준다
        // rigid.AddForce(inputVec); // 방향의 값을 넣음

        // 2. 속도 제어
        // rigid.velocity = inputVec; // 방향의 값을 넣음

        // 3. 위치 이동
        // rigid.MovePosition(rigid.position + inputVec); // 현재 위치에서 방향을 합한 값을 넣어줌
        #endregion

        // Time.fixedDeltaTime, 물리 프레임 하나가 소비한 시간
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        // PlayerInput 을 활용한 입력
        // Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
    }

    // PlayerInput 을 활용한 입력
    void OnMove(InputValue value)
    {
        // inputVec = value.Get<Vector2>();
    }

    // 프레임이 종료 되기 전 실행되는 생명주기 함수
    private void LateUpdate()
    {
        anim.SetFloat("speed", inputVec.magnitude);

        // Sprite Renderer를 활용해서 좌우 반전
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0 ? true : false;
        }
    }
}
