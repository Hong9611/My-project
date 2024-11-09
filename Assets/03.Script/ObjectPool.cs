using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject projectilePrefab;  // ����ü ������
    public int poolSize = 10;            // Ǯ ũ��

    private Queue<GameObject> projectilePool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;

        // ������Ʈ Ǯ �ʱ�ȭ
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            projectilePool.Enqueue(obj);
        }
    }

    // ������Ʈ Ǯ���� ����ü ��������
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

    // ������Ʈ Ǯ�� ����ü ��ȯ
    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectilePool.Enqueue(projectile);
    }
}