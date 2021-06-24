using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager theManager;

    private Dialog loadedDialog;

    [SerializeField]
    Text responseText;

    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    Transform dialogbuttonPanel;

    private void Awake()
    {


        if (theManager == null)
        {
            theManager = this;
        }
        else
        {
            Destroy(this);
        }

        
    }

    public void LoadDialogue(Dialog dialogue)
    {

        transform.GetChild(0).gameObject.SetActive(true);
        loadedDialog = dialogue;
        ClearButtons();
        int i = 0;
        Button spawnedButton;
        foreach (LineofDialog item in dialogue.DialogueOptions)
        {
            float? currentApproval = FactionManager.instance.getFactionsApproval(dialogue.faction);

            if (currentApproval != null && currentApproval > item.minApproval)
            {

            spawnedButton = Instantiate(buttonPrefab, dialogbuttonPanel).GetComponent<Button>();
            spawnedButton.GetComponentInChildren<Text>().text = item.question;

            int i2 = i;
            spawnedButton.onClick.AddListener(delegate { ButtonPressed(i2); });
            i++;
            }
        }

        spawnedButton = Instantiate(buttonPrefab, dialogbuttonPanel).GetComponent<Button>();
        spawnedButton.GetComponentInChildren<Text>().text = dialogue.Goodbye.question;


        spawnedButton.onClick.AddListener(EndConversation);
        DisplayResponse(loadedDialog.greeting);
        

    }

    void EndConversation()
    {
        ClearButtons();
        DisplayResponse(loadedDialog.Goodbye.response);

        //transform.GetChild(0).gameObject.SetActive(false);

        if(loadedDialog.Goodbye.nextDialog != null)
        {
            //loadedDialog = loadedDialog.Goodbye.nextDialog;
            LoadDialogue(loadedDialog.Goodbye.nextDialog);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void ClearButtons()
    {
        foreach (Transform child in dialogbuttonPanel)
        {
            Destroy(child.gameObject);
        }
    }

    void ButtonPressed(int index)
    {
        DisplayResponse(loadedDialog.DialogueOptions[index].response);
        //print(loadedDialog.DialogueOptions[index].response);
    }

    private void DisplayResponse(string response)
    {
        responseText.text = response;
        //Debug.Log(response);
    }


}
