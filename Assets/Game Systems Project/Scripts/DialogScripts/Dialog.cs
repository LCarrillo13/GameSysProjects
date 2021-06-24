using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{

    public string greeting;
    public string faction;


    public bool firstDialog;

    public LineofDialog Goodbye;
    public LineofDialog[] DialogueOptions;

    private void Update()
    {
        if (!firstDialog) return;
        if(Input.GetKeyDown(KeyCode.E))
        {
            DialogManager.theManager.LoadDialogue(this);
        }
    }
}
