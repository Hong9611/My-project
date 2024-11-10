using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
        Debug.Log("ObjectPool initialized.");
    }

    public GameObject GetMonster(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab] = new Queue<GameObject>();
            Debug.Log($"Creating new queue for {prefab.name} in ObjectPool.");
        }

        if (poolDictionary[prefab].Count > 0)
        {
            GameObject monster = poolDictionary[prefab].Dequeue();
            monster.SetActive(true);
            Debug.Log($"{prefab.name} dequeued from pool.");
            return monster;
        }
        else
        {
            GameObject newMonster = Instantiate(prefab);
            Debug.Log($"{prefab.name} instantiated.");
            return newMonster;
        }
    }

    public void ReturnMonster(GameObject monster, GameObject prefab)
    {
        monster.SetActive(false);
        Debug.Log($"{monster.name} returned to pool and deactivated.");

        if (!poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab] = new Queue<GameObject>();
        }
        poolDictionary[prefab].Enqueue(monster);
    }
}