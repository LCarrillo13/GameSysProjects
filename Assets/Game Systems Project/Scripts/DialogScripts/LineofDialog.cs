using UnityEngine;

[System.Serializable]
public class LineofDialog
{
    [TextArea(4,6)]
    public string question, response;

    public float minApproval = -1;

    public Dialog nextDialog;
    [System.NonSerialized]
    public int buttonID;
}
