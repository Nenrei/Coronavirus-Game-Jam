using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    private Transform target;

    [SerializeField] private Animator anim;

    bool raycastToAttack;
    bool raycastToDamage;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("GroundCheck").transform;
        raycastToAttack = true;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = new Vector3(target.position.x, target.position.y, target.position.z);
        Vector3 pos2 = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (raycastToAttack) { 
            RaycastHit2D hit = Physics2D.Raycast(
                        pos2,
                        pos - pos2,
                        8f,
                        1 << LayerMask.NameToLayer("Squirrel")
                    );

            Vector3 forward = transform.TransformDirection(pos - pos2);
            Debug.DrawRay(pos2, forward, Color.red);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    anim.SetTrigger("attack");
                    Invoke("PrepareToAttack", 2.5f);
                    raycastToDamage = true;
                    raycastToAttack = false;

                    Invoke("NoDamage", 0.2f);
                }
            }
        }

        if (raycastToDamage) {
            RaycastHit2D hit = Physics2D.Raycast(
                    pos2,
                    pos - pos2,
                    1f,
                    1 << LayerMask.NameToLayer("Squirrel")
                );

            Vector3 forward = transform.TransformDirection(pos - pos2);
            Debug.DrawRay(pos2, forward, Color.green);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    raycastToDamage = false;
                    GameObject.Find("Squirrel").GetComponent<PlayerHealth>().Damage(1);
                }
            }

        }


    }

    private void PrepareToAttack()
    {
        raycastToAttack = true;
        raycastToDamage = false;
    }

    private void NoDamage()
    {
        raycastToDamage = false;
    }

    private void Hurt()
    {
        raycastToAttack = true;
        GameObject.Find("Squirrel").GetComponent<PlayerHealth>().Damage(3);
    }

}
