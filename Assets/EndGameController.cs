using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{

    [SerializeField] AudioSource playerAudio;
    [SerializeField] PlayerMovement player;
    [SerializeField] Animator playerAnim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem particles;

    [SerializeField] CinemachineVirtualCamera cam;

    [SerializeField] private GameObject uiMessagePanel;
    [SerializeField] private TextMeshProUGUI uiMessage;

    [SerializeField] private GameObject endCinematic;
    [SerializeField] private GameObject faceUI;
    [SerializeField] private GameObject hopeUI;

    [SerializeField] private GameObject credits;

    [SerializeField] private TextMeshProUGUI hopeCount;
    private int hope = 0;

    private bool canGoToMenu;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canGoToMenu && Input.GetKeyDown(KeyCode.Return))
        {
            Application.Quit();
        }
    }

    private IEnumerator LerpAudio()
    {

        yield return new WaitForSeconds(3f);

        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().volume = 0;

        while (GetComponent<AudioSource>().volume < 1)
        {
            playerAudio.volume -= 0.05f;
            GetComponent<AudioSource>().volume += 0.05f;
            yield return new WaitForSeconds(.05f);
        }

        playerAudio.Stop();

    }

        private IEnumerator HopeCount()
    {
        yield return new WaitForSeconds(3f);
        hope = int.Parse(hopeCount.text);
        for (int i = 0; i < 100; i++)
        {
            hope += 1;
            hopeCount.text = hope.ToString();


            hopeCount.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(
                hopeCount.gameObject.GetComponent<RectTransform>().sizeDelta.x,
                hopeCount.gameObject.GetComponent<RectTransform>().sizeDelta.x + 2
            );

            yield return new WaitForSeconds(.08f);

        }

        yield return new WaitForSeconds(2f);

        while (hope > 0)
        {
            hope -= 1;
            hopeCount.text = hope.ToString();
            yield return new WaitForSeconds(.05f);
        }


        yield return new WaitForSeconds(8f);

        while (hopeUI.GetComponent<CanvasGroup>().alpha > 0)
        {
            hope += 1;
            hopeCount.text = hope.ToString();
            hopeUI.GetComponent<CanvasGroup>().alpha -= 0.01f;
            yield return new WaitForSeconds(.05f);
        }
    }

    public IEnumerator EndGame()
    {
        StartCoroutine(HopeCount());
        StartCoroutine(LerpAudio());

        hope = int.Parse(hopeCount.text);
        cam.Follow = playerAnim.gameObject.transform;
        particles.gameObject.SetActive(true);

        player.canMove = false;

        rb.velocity = Vector2.zero;


        yield return new WaitForSeconds(0.1f);
        cam.m_Lens.OrthographicSize = 13;

        yield return new WaitForSeconds(0.1f);
        cam.m_Lens.OrthographicSize = 12;


        yield return new WaitForSeconds(0.1f);
        cam.m_Lens.OrthographicSize = 11;

        yield return new WaitForSeconds(0.1f);
        cam.m_Lens.OrthographicSize = 10;

        yield return new WaitUntil(() => rb.velocity.y == 0f);

        playerAnim.SetBool("running", false);
        playerAnim.SetBool("grounded", true);


        yield return new WaitForSeconds(0.2f);
        playerAnim.SetTrigger("endGame");


        yield return new WaitForSeconds(0.25f);
        faceUI.SetActive(false);
        particles.Play();


        yield return new WaitForSeconds(1f);
        particles.gameObject.SetActive(false);


        yield return new WaitForSeconds(2f);

        cam.Follow = null;

        yield return new WaitForSeconds(1f);

        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 1;
        uiMessage.text = "Great! You found a living forest!";

        yield return new WaitForSeconds(2f);
        uiMessage.text = "Wait...";


        yield return new WaitForSeconds(2f);
        uiMessage.text = "Did you?";

        yield return new WaitForSeconds(1f);
        endCinematic.SetActive(true);

        yield return new WaitForSeconds(.3f);
        cam.m_Lens.OrthographicSize = 14;
        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 0;



        yield return new WaitForSeconds(22f);

        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 1;
        uiMessage.text = "When all seems lost, there's still hope";


        yield return new WaitForSeconds(3f);
        uiMessage.text = "Thank you for playing!";

        yield return new WaitForSeconds(3f);
        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 0;

        credits.SetActive(true);
        while (credits.GetComponent<CanvasGroup>().alpha < 1)
        {
            credits.GetComponent<CanvasGroup>().alpha += 0.01f;
            yield return new WaitForSeconds(.05f);
        }
        canGoToMenu = true;


        StopAllCoroutines();

    }

}
