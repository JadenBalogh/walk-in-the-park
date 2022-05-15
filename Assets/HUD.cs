using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    private static HUD instance;

    [SerializeField] private TextMeshProUGUI tooltip;
    [SerializeField] private DialogueMenu dialogueMenu;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        SetTooltipActive(false);
        SetDialogueActive(false);
    }

    public static void SetTooltipTarget(Interactable target) => instance.ISetTooltipTarget(target);

    private void ISetTooltipTarget(Interactable target)
    {
        tooltip.rectTransform.position = Camera.main.WorldToScreenPoint(target.GetTooltipPosition());
        tooltip.text = target.GetTooltipText();
    }

    public static void SetTooltipActive(bool active) => instance.ISetTooltipActive(active);

    private void ISetTooltipActive(bool active)
    {
        tooltip.GetComponent<CanvasGroup>().alpha = active ? 1 : 0;
    }

    public static void SetDialogueActive(bool active) => instance.ISetDialogueActive(active);

    private void ISetDialogueActive(bool active)
    {
        dialogueMenu.SetActive(active);
    }

    public static void SetDialogueText(string text) => instance.ISetDialogueText(text);

    private void ISetDialogueText(string text)
    {
        dialogueMenu.SetText(text);
    }
}
