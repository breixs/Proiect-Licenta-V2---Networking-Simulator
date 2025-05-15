using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalManager : MonoBehaviour
{
    public GameObject directoryLine;
    public GameObject responseLine;

    public TMP_InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect sr;
    public GameObject commandLineContainer;
    //public TextMeshPro deviceText;
    private string userInput;

    //public GameObject laptop;
    //private GameObject connectedDevice;

    //private short counter = 0;

    Interpreter interpreter;

    private void Start()
    {
        interpreter=GetComponent<Interpreter>();
    }

    private void OnEnable()
    {
        commandLineContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(1772f, 867f);
        sr.verticalNormalizedPosition = 1f;
    }

    private void OnGUI()
    {
        //preia comanda trecuta si o copiaza in input text box
        if (userInputLine != null && Input.GetKeyDown(KeyCode.UpArrow) && terminalInput.text == "")
        {
            terminalInput.text = userInput;
        }

        if (terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {

            userInput = terminalInput.text;

            ClearInputField();

            AddDirectoryLine(userInput);

            int lines = AddInterpreterLines(interpreter.Interpret(userInput));

            //ScrollToBottom(lines);

            userInputLine.transform.SetAsLastSibling();

            terminalInput.ActivateInputField();
            terminalInput.Select();
            //if (counter < 19)
            //{
            //    sr.verticalNormalizedPosition = 0.5f;
            //    counter++;
            //}
            //else
            //{
            //    sr.verticalNormalizedPosition = 0.5f;
            //}
        }


    }

    private void ClearInputField()
    {
        terminalInput.text = "";
    }

    private void AddDirectoryLine(string userInput)
    {
        directoryLine.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = interpreter.deviceText.text;

        Vector2 msgListSize = commandLineContainer.GetComponent<RectTransform>().sizeDelta;
        commandLineContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 35.0f);

        GameObject msg = Instantiate(directoryLine, commandLineContainer.transform);

        msg.transform.SetSiblingIndex(commandLineContainer.transform.childCount - 1);

        msg.GetComponentsInChildren<TextMeshProUGUI>()[1].text = userInput;
    }

    private int AddInterpreterLines(List<string> interpretation)
    {
        for(int i=0;i<interpretation.Count;i++)
        {
            GameObject resp = Instantiate(responseLine, commandLineContainer.transform);

            resp.transform.SetAsLastSibling();

            Vector2 listSize=commandLineContainer.GetComponent<RectTransform>().sizeDelta;
            commandLineContainer.GetComponent<RectTransform>().sizeDelta= new Vector2(listSize.x, listSize.y + 35.0f);
            resp.GetComponentInChildren<TextMeshProUGUI>().text = interpretation[i];


        }
        return interpretation.Count;
    }

    void ScrollToBottom(int lines)
    {
        if(lines>3)
        {
            sr.velocity = new Vector2(0, 450);
        }
        else
        {
            sr.verticalNormalizedPosition = 0f;
        }
    }

    public void ClearTerminal()
    {
        foreach (Transform child in commandLineContainer.transform)
        {
            // Skip the input line and any object that isn't a clone
            if (child.gameObject == userInputLine ||
                !child.name.Contains("(Clone)")) // Only delete clones
            {
                continue;
            }

            Destroy(child.gameObject);
        }

        // Reset scroll height
        commandLineContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(
            commandLineContainer.GetComponent<RectTransform>().sizeDelta.x,
            0f
        );

        // Scroll to top
        sr.verticalNormalizedPosition = 1f;
    }
}
