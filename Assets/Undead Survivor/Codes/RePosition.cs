using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class RePosition : MonoBehaviour
{
    // 몬스터가 너무 멀어지는 경우 위치 재조정
    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area")) return;
        
        // 플레이어와 맵의 좌표
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "ground": // 땅인경우
                if (diffX > diffY) // 맵을 수평으로 이동되게 하는 경우
                {
                    transform.Translate(Vector3.right * dirX * (20 * 2));
                }
                else if (diffX < diffY) // 맵을 수직으로 이동되게 하는 경우
                {
                    transform.Translate(Vector3.up * dirY * (20 * 2));
                }
                break;
            case "Enemy": // 적인경우
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
            default:
                break;
        }
    }
}
