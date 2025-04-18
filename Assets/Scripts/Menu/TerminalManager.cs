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
    public GameObject msgList;
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

    //private void OnEnable()
    //{
    //    if (laptop.transform.childCount > 1)
    //    {
    //        deviceText.text = laptop.transform.GetChild(1).name;
    //        connectedDevice = CheckCableParents.instance.getConsoleStartNodeParent();
    //        if (connectedDevice == null)
    //        {
    //            connectedDevice = CheckCableParents.instance.getConsoleEndNodeParent();
    //            if (connectedDevice == null)
    //            {
    //                CloseTerminal();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        CloseTerminal();
    //    }
    //}

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

        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 35.0f);

        GameObject msg = Instantiate(directoryLine, msgList.transform);

        msg.transform.SetSiblingIndex(msgList.transform.childCount - 1);

        msg.GetComponentsInChildren<TextMeshProUGUI>()[1].text = userInput;
    }

    private int AddInterpreterLines(List<string> interpretation)
    {
        for(int i=0;i<interpretation.Count;i++)
        {
            GameObject resp = Instantiate(responseLine, msgList.transform);

            resp.transform.SetAsLastSibling();

            Vector2 listSize=msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta= new Vector2(listSize.x, listSize.y + 35.0f);
            resp.GetComponentInChildren<TextMeshProUGUI>().text = interpretation[i];


        }
        return interpretation.Count;
    }

    void ScrollToBottom(int lines)
    {
        if(lines>4)
        {
            sr.velocity = new Vector2(0, 450);
        }
        else
        {
            sr.verticalNormalizedPosition = 0f;
        }
    }

    //private void CloseTerminal()
    //{
    //    gameObject.SetActive(false);
    //    PlayerUI.inTerminal = false;
    //    Cursor.visible = false;
    //}
}
