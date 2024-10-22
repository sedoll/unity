using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnDatas;
    
    int level;
    float spawnTimer;

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        try
        {
            spawnTimer += Time.deltaTime; // 한 프레임 마다 시간을 더함
            level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnDatas.Length - 1); // 최대 값이 넘으면 spawnDatas의 최대 값으로 맞춤
            if (spawnTimer > spawnDatas[level].spawnTime) // 스폰 시간
            {
                spawnTimer = 0;
                Spawn();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    void Spawn()
    {
       GameObject enemy = GameManager.instance.pool.Get(0); // level에 따른 적 생성
       enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position; // 자기 자신의 요소를 제외 하기 위해 1부터 시작
       enemy.GetComponent<Enemy>().Init(spawnDatas[level]);
    }
}

[System.Serializable] // unity 에디터에서 보이도록 직렬화
public class SpawnData
{
    public int spriteType; // 스프라이트 타입
    public float spawnTime; // 소환시간
    public int health; // 체력
    public float speed; // 속도
}
