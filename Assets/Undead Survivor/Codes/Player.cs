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
    
    // �ʱ�ȭ
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // ó���� ������ �� �����ϴ� �Լ�
    void Start()
    {
        
    }

    // �� ������ ���� ����Ǵ� �Լ�
    void Update()
    {
        // GetAxis, �������� �� �� �ε巴�� �̲����� ������ ��
        // inputVec.x = Input.GetAxisRaw("Horizontal"); // ����
        // inputVec.y = Input.GetAxisRaw("Vertical"); // ������

        // GetAxisRaw, �� �ε巴�� ������ ��Ʈ���� ����
        inputVec.x = Input.GetAxisRaw("Horizontal"); // ����
        inputVec.y = Input.GetAxisRaw("Vertical"); // ������
    }

    // ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    private void FixedUpdate()
    {
        #region �����̴� ��� ������
        // 1. ���� �ش�
        // rigid.AddForce(inputVec); // ������ ���� ����

        // 2. �ӵ� ����
        // rigid.velocity = inputVec; // ������ ���� ����

        // 3. ��ġ �̵�
        // rigid.MovePosition(rigid.position + inputVec); // ���� ��ġ���� ������ ���� ���� �־���
        #endregion

        // Time.fixedDeltaTime, ���� ������ �ϳ��� �Һ��� �ð�
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        // PlayerInput �� Ȱ���� �Է�
        // Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
    }

    // PlayerInput �� Ȱ���� �Է�
    void OnMove(InputValue value)
    {
        // inputVec = value.Get<Vector2>();
    }

    // �������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
    private void LateUpdate()
    {
        anim.SetFloat("speed", inputVec.magnitude);

        // Sprite Renderer�� Ȱ���ؼ� �¿� ����
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0 ? true : false;
        }
    }
}
