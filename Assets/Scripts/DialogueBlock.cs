using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueBlock", fileName = "DialogueBlock", order = 51)]
public class DialogueBlock : ScriptableObject
{
    public string[] dialogueMessages;
    public DialogueEvent[] dialogueEvents;

    public string GetMessage(int index)
    {
        return dialogueMessages[index];
    }

    public void CompleteDialogue()
    {
        foreach (DialogueEvent dialogueEvent in dialogueEvents)
        {
            NPC target = GameObject.Find(dialogueEvent.targetName).GetComponent<NPC>();
            target.SetDialogueBlockIndex(dialogueEvent.targetDialogueIndex);
            target.SetPatrolIndex(dialogueEvent.targetPatrolIndex);
        }
    }

    public int GetLength()
    {
        return dialogueMessages.Length;
    }

    [System.Serializable]
    public class DialogueEvent
    {
        public string targetName = "";
        public int targetDialogueIndex;
        public int targetPatrolIndex;
    }
}
