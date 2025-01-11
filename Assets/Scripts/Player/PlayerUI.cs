using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI taskText;
    //private CheckCableParents parentScript;

    // Start is called before the first frame update
    void Start()
    {
        /*parentScript = GetComponent<CheckCableParents>();
        taskText.text="Task : connect "+ parentScript.GetNames();*/
        promptText.gameObject.SetActive(false);
        
    }
    public void UpdateText(string promptMessage)
    {
        promptText.gameObject.SetActive(true);
        promptText.text = promptMessage;
    }
    public void DeactivateText()
    {
        promptText.gameObject.SetActive(false);
    }
}
