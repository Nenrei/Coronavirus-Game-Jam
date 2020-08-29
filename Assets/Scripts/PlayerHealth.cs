using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 1f;
    public TextMeshProUGUI healthCount;

    public void AddHealth(float toAdd)
    {
        health += toAdd;
        healthCount.text = health.ToString();
    }

    public void Damage(float damage)
    {
        health -= damage;
        healthCount.text = health.ToString();

        if (health <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
