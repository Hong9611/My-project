using UnityEngine;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint Instance;
    public List<GameObject> monsterPrefabs;
    private int currentMonsterIndex = 0;

    private void Awake()
    {
        Instance = this;
        Debug.Log("SpawnPoint initialized.");
    }

    private void Start()
    {
        SpawnNextMonster();
    }

    public void SpawnNextMonster()
    {
        if (GameManager.Instance.monsterDataList.Count > 0)
        {
            MonsterData data = GameManager.Instance.monsterDataList[currentMonsterIndex];
            GameObject prefab = monsterPrefabs[currentMonsterIndex];

            GameObject monster = ObjectPool.Instance.GetMonster(prefab);
            monster.transform.position = transform.position;

            Monster monsterScript = monster.GetComponent<Monster>();
            monsterScript.Initialize(data);
            monsterScript.OnDeath += OnMonsterDeath;  // 몬스터 사망 시 OnMonsterDeath 호출

            Debug.Log($"{data.name} initialized and spawned at {transform.position}");

            currentMonsterIndex = (currentMonsterIndex + 1) % monsterPrefabs.Count;
        }
        else
        {
            Debug.LogError("No monster data found. MonsterDataLoader did not load data correctly.");
        }
    }

    private void OnMonsterDeath(GameObject monster)
    {
        GameObject prefab = monsterPrefabs[(currentMonsterIndex + monsterPrefabs.Count - 1) % monsterPrefabs.Count];
        ObjectPool.Instance.ReturnMonster(monster, prefab);

        SpawnNextMonster();
    }
}
