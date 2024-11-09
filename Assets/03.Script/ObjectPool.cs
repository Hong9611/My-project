using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject projectilePrefab;  // 투사체 프리팹
    public int poolSize = 10;            // 풀 크기

    private Queue<GameObject> projectilePool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;

        // 오브젝트 풀 초기화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            projectilePool.Enqueue(obj);
        }
    }

    // 오브젝트 풀에서 투사체 가져오기
    public GameObject GetProjectile()
    {
        if (projectilePool.Count > 0)
        {
            GameObject obj = projectilePool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(projectilePrefab);
            return obj;
        }
    }

    // 오브젝트 풀로 투사체 반환
    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectilePool.Enqueue(projectile);
    }
}