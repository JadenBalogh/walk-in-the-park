using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private Animation2D idleAnim;
    [SerializeField] private Animation2D moveAnim;
    [SerializeField] private PatrolPath[] patrolPaths;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private DialogueBlock[] dialogueBlocks;

    private int currTarget = 0;
    private bool isIdle = false;
    private int patrolIndex = 0;
    private int dialogueBlockIndex = 0;
    private int dialogueIndex = 0;

    private new Rigidbody2D rigidbody2D;
    private Animator2D animator2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator2D = GetComponent<Animator2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PatrolPoint currPoint = patrolPaths[patrolIndex].patrolPoints[currTarget];
        Vector2 moveVector = currPoint.point.position - transform.position;

        if (moveVector.sqrMagnitude < 0.1f)
        {
            StartCoroutine(IdleTimer(currPoint.idleTime));
            currTarget = (currTarget + 1) % patrolPaths[patrolIndex].patrolPoints.Length;
        }

        if (isIdle || IsInteracting)
        {
            rigidbody2D.velocity = Vector2.zero;
            animator2D.Play(idleAnim, true);
        }
        else
        {
            rigidbody2D.velocity = moveVector.normalized * moveSpeed;
            animator2D.Play(moveAnim, true);
        }

        spriteRenderer.flipX = moveVector.x > 0;
    }

    public override void Use()
    {
        if (!IsInteracting)
        {
            IsInteracting = true;
            HUD.SetDialogueActive(true);
            HUD.SetDialogueText(dialogueBlocks[dialogueBlockIndex].GetMessage(dialogueIndex));
            return;
        }

        dialogueIndex++;

        if (dialogueIndex >= dialogueBlocks[dialogueBlockIndex].GetLength())
        {
            dialogueIndex = 0;
            IsInteracting = false;
            dialogueBlocks[dialogueBlockIndex].CompleteDialogue();
            HUD.SetDialogueActive(false);
            return;
        }

        HUD.SetDialogueText(dialogueBlocks[dialogueBlockIndex].GetMessage(dialogueIndex));
    }

    public void SetPatrolIndex(int index)
    {
        this.patrolIndex = index;
    }

    public void SetDialogueBlockIndex(int index)
    {
        this.dialogueBlockIndex = index;
    }

    private IEnumerator IdleTimer(float idleTime)
    {
        isIdle = true;
        yield return new WaitForSeconds(idleTime);
        isIdle = false;
    }

    [System.Serializable]
    private class PatrolPath
    {
        public PatrolPoint[] patrolPoints;
    }

    [System.Serializable]
    private class PatrolPoint
    {
        public Transform point;
        public float idleTime = 0f;
    }
}
