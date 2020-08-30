using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator anim;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    PlayerHealth health;
    PlayerHope hope;

    public bool canMove = true;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        hope = GetComponent<PlayerHope>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        anim.SetBool("running", horizontalMove != 0);    
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Nut"))
        {
            Destroy(collision.gameObject);
            health.AddHealth(1);

        }else if (collision.gameObject.CompareTag("Hope"))
        {
            Destroy(collision.gameObject);
            hope.AddHope(1);

        }else if (collision.gameObject.CompareTag("Water"))
        {
            health.Damage(GetComponent<PlayerHealth>().health);

        }
    }


}
