using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MonsterData : MonoBehaviour
{
    public List<MonsterInfo> monsterInfos = new List<MonsterInfo>();

    void Start()
    {
        LoadMonsterData();
    }

    void LoadMonsterData()
    {
        string path = "Assets/Data/MonsterData.csv"; // CSV 파일 경로
        string[] lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            string[] values = line.Split(',');
            MonsterInfo info = new MonsterInfo
            {
                name = values[0],
                grade = values[1],
                speed = float.Parse(values[2]),
                health = int.Parse(values[3])
            };
            monsterInfos.Add(info);
        }
    }
}

public class MonsterInfo
{
    public string name;
    public string grade;
    public float speed;
    public int health;
}