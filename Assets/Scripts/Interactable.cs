using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private Vector2 tooltipOffset;
    [SerializeField] private string tooltipText;

    public bool IsInteracting { get; protected set; }

    public void SetInteractionActive(bool active)
    {
        HUD.SetTooltipTarget(this);
        HUD.SetTooltipActive(active && !IsInteracting);
    }

    public abstract void Use();

    public Vector3 GetTooltipPosition()
    {
        return transform.position + (Vector3)tooltipOffset;
    }

    public string GetTooltipText()
    {
        return "[E] " + tooltipText;
    }
}
