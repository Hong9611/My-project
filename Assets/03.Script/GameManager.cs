using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using System;

[System.Serializable]
public class MonsterData
{
    public string name;
    public string grade;
    public float speed;
    public int maxHealth;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<MonsterData> monsterDataList = new List<MonsterData>();

    public Transform arrivePosition;

    void Awake()
    {
        Instance = this;
        LoadMonsterData("Assets/04.Data/SampleMonster.csv");
    }

    void LoadMonsterData(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath, System.Text.Encoding.GetEncoding("EUC-KR"));
            for (int i = 1; i < lines.Length; i++)  // 첫 줄을 건너뛰기 위해 인덱스를 1부터 시작
            {
                string line = lines[i];
                string[] values = line.Split(',');

                MonsterData data = new MonsterData
                {
                    name = values[0].Trim(),
                    grade = values[1].Trim(),
                    speed = float.Parse(values[2].Trim(), CultureInfo.InvariantCulture),
                    maxHealth = int.Parse(values[3].Trim())
                };
                monsterDataList.Add(data);

                Debug.Log($"Loaded data for {data.name}: Speed={data.speed}, MaxHealth={data.maxHealth}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading monster data: {e.Message}");
        }
    }
}