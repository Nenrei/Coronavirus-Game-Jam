using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHowTo : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] texts;
    [SerializeField] private CanvasGroup[] images;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(StartAnimationTexts());
        StartCoroutine(StartAnimationImages());
    }

    private IEnumerator StartAnimationTexts()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(4.5f);
            texts[i].gameObject.SetActive(false);
        }


        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator StartAnimationImages()
    {
        yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(4.5f * 5);

        images[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(5.5f);

        images[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        images[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        images[3].gameObject.SetActive(true);
    }
}
