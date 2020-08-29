using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 1f;
    public TextMeshProUGUI healthCount;

    private bool takingDamage;

    [SerializeField] Vector2 hurtSpeed = new Vector2(-200f, 20f);

    public void AddHealth(float toAdd)
    {
        health += toAdd;
        healthCount.text = health.ToString();
    }

    public void Damage(float damage)
    {
        if (!takingDamage)
        {
            takingDamage = true;
            health -= damage;
            healthCount.text = health.ToString();


            if (health <= 0)
            {
                //KillPlayer();
            }
            Invoke("AllowTakeDamate", 0.1f);
        }
    }

    private void KillPlayer()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void AllowTakeDamate()
    {
        GetComponent<Rigidbody2D>().AddForce(hurtSpeed, ForceMode2D.Impulse);
        takingDamage = false;
    }

}
