using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 2 * 10f; // 20초
    
    public PoolManager pool; // 몹 소환
    public Player player; // 플레이어

    private void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        gameTime += Time.deltaTime; // 한 프레임 마다 시간을 더함

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
