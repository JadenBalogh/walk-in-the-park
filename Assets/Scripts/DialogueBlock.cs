using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueBlock", fileName = "DialogueBlock", order = 51)]
public class DialogueBlock : ScriptableObject
{
    public string[] dialogueMessages;

    public string GetMessage(int index)
    {
        return dialogueMessages[index];
    }

    public int GetLength()
    {
        return dialogueMessages.Length;
    }
}
