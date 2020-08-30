using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquirrelFace : MonoBehaviour
{

    [SerializeField] private Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFace(float health)
    {
        if(health <= 0)
        {
            GetComponent<Image>().sprite = sprites[0];
        }else if (health >= 4)
        {
            GetComponent<Image>().sprite = sprites[3];
        }
        else
        {
            GetComponent<Image>().sprite = sprites[(int)health - 1];
        }


    }
}
