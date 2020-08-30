using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHope : MonoBehaviour
{
    public float hope = 0f;

    public TextMeshProUGUI hopeCount;
    private void Awake()
    {
        AddHope(0);
    }

    public void AddHope(float toAdd)
    {
        hope += toAdd;
        hopeCount.text = hope.ToString();
    }

    public void DecreaseHope(float toDecrease)
    {
        hope -= toDecrease;
        hopeCount.text = hope.ToString();
    }
}
