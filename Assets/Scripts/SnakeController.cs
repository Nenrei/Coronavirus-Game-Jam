using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    private Transform target;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform rayCastPoint;

    [SerializeField] private bool raycastToAttack;
    [SerializeField] private bool raycastToDamage;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("GroundCheck").transform;
        raycastToAttack = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (raycastToAttack) { 
            RaycastHit2D hit = Physics2D.Raycast(
                        rayCastPoint.position,
                        target.position - rayCastPoint.position,
                        8f,
                        1 << LayerMask.NameToLayer("Squirrel")
                    );

            Vector3 forward = rayCastPoint.TransformDirection(target.position - rayCastPoint.position);
            //Debug.DrawRay(pos2, forward, Color.red);

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
                    rayCastPoint.position,
                    target.position - rayCastPoint.position,
                    1f,
                    1 << LayerMask.NameToLayer("Squirrel")
                );

            Vector3 forward = transform.TransformDirection(target.position - rayCastPoint.position);
            Debug.DrawRay(rayCastPoint.position, forward, Color.green);

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


}
