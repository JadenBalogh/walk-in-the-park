using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textbox;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetActive(bool active)
    {
        canvasGroup.alpha = active ? 1 : 0;
    }

    public void SetText(string text)
    {
        textbox.text = text;
    }
}
