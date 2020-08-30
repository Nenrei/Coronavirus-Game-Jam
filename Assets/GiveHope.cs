using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GiveHope : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Show()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<CanvasGroup>().alpha = 1;
    }
    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }

}
