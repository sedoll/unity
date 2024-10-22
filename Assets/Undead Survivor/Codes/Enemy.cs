using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    private bool isLive = true; // 죽었는지 살았는지 확인

    private Rigidbody2D rigid;
    Animator animator;
    private SpriteRenderer spriter;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive) return; // 몬스터가 죽은경우는 실행 안함
        Vector2 dirVec = target.position - rigid.position; // 방향
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리가 이동속도에 영향받지 않도록 0으로 설정
    }

    void LateUpdate()
    {
        if (!isLive) return; // 몬스터가 죽은경우는 실행 안함
        // 플레이어 위치에 따른 몬스터의 좌, 우 방향 설정
        spriter.flipX = target.position.x < rigid.position.x;
    }
    
    // 활성화 될때 자동으로 실행되는 함수
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); // 생성되고 player으로 타겟을 초기화
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
