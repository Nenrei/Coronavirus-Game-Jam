using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject optionArrow;
    [SerializeField] private GameObject[] options;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private float[] optionsYPosition = { 0, 0, 0};
    [SerializeField] private GameObject howToPanel;

    private int selectedOption = 0;

    private bool canSelect = true;

    private bool gameStarted;




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < options.Length; i++)
        {
            optionsYPosition[i] = options[i].GetComponent<RectTransform>().position.y;
        }
        options[selectedOption].GetComponent<Animator>().SetBool("selected", true);
        options[selectedOption].GetComponent<TextMeshProUGUI>().color = selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            bool up = Input.GetAxisRaw("Vertical") == 1;
            bool down = Input.GetAxisRaw("Vertical") == -1;
            int newSelectedOption = selectedOption;

            if (canSelect && (up || down))
            {
                if (up && selectedOption == 0)
                {
                    newSelectedOption = 1;
                }
                else if (down && selectedOption == 1)
                {
                    newSelectedOption = 0;
                }
                else
                {
                    if (up)
                    {
                        newSelectedOption -= 1;
                    }
                    else if (down)
                    {
                        newSelectedOption += 1;
                    }
                }
                if (newSelectedOption != selectedOption)
                {
                    optionArrow.GetComponent<RectTransform>().position = new Vector3(optionArrow.GetComponent<RectTransform>().position.x, optionsYPosition[newSelectedOption], 0f);


                    options[selectedOption].GetComponent<Animator>().SetBool("selected", false);
                    options[selectedOption].GetComponent<TextMeshProUGUI>().color = normalColor;

                    options[newSelectedOption].GetComponent<Animator>().SetBool("selected", true);
                    options[newSelectedOption].GetComponent<TextMeshProUGUI>().color = selectedColor;


                    selectedOption = newSelectedOption;

                }
                canSelect = false;
                Invoke("AllowSelect", 0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                SelectOption();
            }
        }
    }

    private void AllowSelect()
    {
        canSelect = true;
    }

    private void SelectOption()
    {
        switch (selectedOption)
        {
            case 0:
                gameStarted = true;
                howToPanel.SetActive(true);
                break;
            case 1:
                Application.Quit();
                break;
        }
    }
}
