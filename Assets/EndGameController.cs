using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] Animator playerAnim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem particles;

    [SerializeField] CinemachineVirtualCamera cam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator EndGame()
    {
        cam.Follow = playerAnim.gameObject.transform;
        particles.gameObject.SetActive(true);

        player.canMove = false;

        rb.velocity = Vector2.zero;


        yield return new WaitForSeconds(0.1f);
        cam.m_Lens.OrthographicSize--;

        yield return new WaitForSeconds(0.1f);
        cam.m_Lens.OrthographicSize--;


        yield return new WaitForSeconds(0.1f);
        cam.m_Lens.OrthographicSize--;

        yield return new WaitForSeconds(0.1f);
        cam.m_Lens.OrthographicSize--;

        yield return new WaitUntil(() => rb.velocity.y == 0f);

        playerAnim.SetBool("running", false);
        playerAnim.SetBool("grounded", true);


        yield return new WaitForSeconds(0.2f);
        playerAnim.SetTrigger("endGame");


        yield return new WaitForSeconds(0.25f);
        particles.Play();


        yield return new WaitForSeconds(1f);
        particles.gameObject.SetActive(false);

        cam.Follow = null;

        //playerAnim.SetBool("running", false);

    }
}
