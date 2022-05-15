using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private Animation2D idleAnim;
    [SerializeField] private Animation2D moveAnim;
    [SerializeField] private PatrolPoint[] patrolPoints;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private DialogueBlock dialogueBlock;

    private int currTarget = 0;
    private bool isIdle = false;
    private bool isDialogueActive = false;
    private int dialogueIndex = 0;

    private new Rigidbody2D rigidbody2D;
    private Animator2D animator2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator2D = GetComponent<Animator2D>();
    }

    private void Update()
    {
        PatrolPoint currPoint = patrolPoints[currTarget];
        Vector2 moveVector = currPoint.point.position - transform.position;

        if (moveVector.sqrMagnitude < 0.1f)
        {
            StartCoroutine(IdleTimer(currPoint.idleTime));
            currTarget = (currTarget + 1) % patrolPoints.Length;
        }

        if (isIdle || isDialogueActive)
        {
            rigidbody2D.velocity = Vector2.zero;
            animator2D.Play(idleAnim, true);
        }
        else
        {
            rigidbody2D.velocity = moveVector.normalized * moveSpeed;
            animator2D.Play(moveAnim, true);
        }
    }

    public override void Use()
    {
        if (!isDialogueActive)
        {
            isDialogueActive = true;
            HUD.SetDialogueActive(true);
            HUD.SetDialogueText(dialogueBlock.GetMessage(dialogueIndex));
            return;
        }

        dialogueIndex++;

        if (dialogueIndex >= dialogueBlock.GetLength())
        {
            dialogueIndex = 0;
            isDialogueActive = false;
            HUD.SetDialogueActive(false);
            return;
        }

        HUD.SetDialogueText(dialogueBlock.GetMessage(dialogueIndex));
    }

    private IEnumerator IdleTimer(float idleTime)
    {
        isIdle = true;
        yield return new WaitForSeconds(idleTime);
        isIdle = false;
    }

    [System.Serializable]
    private class PatrolPoint
    {
        public Transform point;
        public float idleTime = 0f;
    }
}
