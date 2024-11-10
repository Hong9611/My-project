using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI hp;

    public static PopUp Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DataWrite(string Name, float Speed, int Hp)
    {
        name.text = $"Name:{Name}";
        speed.text = $"Speed:{Speed}";
        hp.text = $"MaxHp:{Hp}";
    }
}
