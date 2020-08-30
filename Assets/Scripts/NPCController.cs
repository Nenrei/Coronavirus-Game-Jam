using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private GiveHope giveHopeUI;
    private RectTransform giveHopeUITransform;
    [SerializeField] private TextMeshProUGUI hopeCount;

    public float hopePrice = 0f;

    bool showUI;

    public Vector2 uiOffset = new Vector2(-8.5f, 4f);

    public ParticleSystem particles;

    public string hability;

    private GameObject player;

    private Animator anim;


    [SerializeField] private GameObject uiMessagePanel;
    [SerializeField] private TextMeshProUGUI uiMessage;



    // Start is called before the first frame update
    void Start()
    {
        giveHopeUI = GameObject.Find("GiveHopeUI").GetComponent<GiveHope>();
        giveHopeUITransform = GameObject.Find("GiveHopeUI").GetComponent<RectTransform>();

        player = GameObject.Find("Squirrel");

        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (showUI)
        {
            Vector3 position = Vector3.zero;
            position.x = transform.position.x + uiOffset.x;
            position.y = transform.position.y + uiOffset.y;
            giveHopeUITransform.position = RectTransformUtility.WorldToScreenPoint(GameObject.Find("Main Camera").GetComponent<Camera>(), position);
            hopeCount.text = "x"+hopePrice.ToString();

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hopePrice > player.GetComponent<PlayerHope>().hope)
                {
                    StartCoroutine(ShowHideMessage("You don't have enough Hope"));
                }
                else
                {
                    player.GetComponent<PlayerHope>().DecreaseHope(hopePrice);
                    showUI = false;
                    UnlockHability();
                }
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        showUI = true;
        StartCoroutine(giveHopeUI.Show());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        showUI = false;
        giveHopeUI.Hide();
    }

    private void UnlockHability()
    {
        if(hability == "Double Jump")
        {
            player.GetComponent<CharacterController2D>().doubleJump = true;
            player.GetComponent<PlayerMovement>().canMove = false;
            anim.SetTrigger("glowUp");
            GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = transform;
            
            StartCoroutine(GetBackPlayerControlAfterJump("You learn how to double jump!"));
        }

        if (hability == "Fly")
        {
            player.GetComponent<CharacterController2D>().fly = true;
            player.GetComponent<PlayerMovement>().canMove = false;
            anim.SetTrigger("glowUp");
            GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = transform;

            StartCoroutine(GetBackPlayerControlAfterFly("You learn how to fly!<br>Hold Jump on a double jump to fly"));
        }
    }

    private IEnumerator GetBackPlayerControlAfterJump(string message)
    {
        anim.SetTrigger("glowUp");

        yield return new WaitForSeconds(0.3f);
        particles.Play();

        yield return new WaitForSeconds(1f);
        anim.SetTrigger("doubleJump");

        yield return new WaitForSeconds(1.8f);
        particles.Play();

        yield return new WaitForSeconds(0.35f);
        particles.Play();

        yield return new WaitForSeconds(1f);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = null;

        yield return new WaitForSeconds(0.5f);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        player.GetComponent<PlayerMovement>().canMove = true;

        yield return new WaitForSeconds(0.5f);
        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 1;
        uiMessage.text = message;

        yield return new WaitForSeconds(2.5f);
        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 0;

        gameObject.SetActive(false);
        
    }

    private IEnumerator GetBackPlayerControlAfterFly(string message)
    {
        anim.SetTrigger("glowUp");

        yield return new WaitForSeconds(0.3f);
        particles.Play();

        yield return new WaitForSeconds(1f);
        anim.SetTrigger("fly");

        yield return new WaitForSeconds(1.5f);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = null;

        yield return new WaitForSeconds(1.5f);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        player.GetComponent<PlayerMovement>().canMove = true;

        yield return new WaitForSeconds(0.5f);
        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 1;
        uiMessage.text = message;

        yield return new WaitForSeconds(3.5f);
        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 0;


        gameObject.SetActive(false);
    }

    private IEnumerator ShowHideMessage(string message)
    {
        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 1;
        uiMessage.text = message;

        yield return new WaitForSeconds(2.5f);
        uiMessagePanel.GetComponent<CanvasGroup>().alpha = 0;
    }


}
