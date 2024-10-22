using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 1. 프리펩들을 보관할 변수가 필요
    public GameObject[] prefabs;
    
    // 2. 풀을 담당하는 리스트들이 필요, 변수의 개수와 리스트의 개수는 동일하게 만들어야 한다.
    private List<GameObject>[] pools; // 리스트 배열 형태

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
        // Debug.Log(pools.Length);
    }

    public GameObject Get(int idx)
    {
        GameObject select = null;
        
        // 1. 선택한 풀의 놀고 있는 게임오브젝트 접근
        foreach (GameObject pool in pools[idx])
        {
            if (!pool.activeSelf) // 비활성화 상태인 경우
            {
                // 2. 놀고 있는 풀을 발견하면 select 변수에 할당
                select = pool;
                select.SetActive(true); // 활성화
                break; // 반복문 종료
            }
        }
        
        // 3. 못 찾을 경우
        if (!select)
        {
            // 4. 새로 생성하고 select에 할당
            select = Instantiate(prefabs[idx], transform);
            pools[idx].Add(select);
        }
        
        return select;
    }
}
