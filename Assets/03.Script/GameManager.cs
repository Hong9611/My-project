using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public Transform spawnPoint;

    private int currentMonsterIndex = 0;

    void Start()
    {
        SpawnMonster();
    }

    void SpawnMonster()
    {
        GameObject monster = Instantiate(monsterPrefabs[currentMonsterIndex], spawnPoint.position, Quaternion.identity);
        currentMonsterIndex = (currentMonsterIndex + 1) % monsterPrefabs.Length;
    }
}