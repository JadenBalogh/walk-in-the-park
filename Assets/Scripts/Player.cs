using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animation2D idleAnim;
    [SerializeField] private Animation2D moveAnim;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float interactDist = 1f;
    [SerializeField] private LayerMask interactLayer;

    private Interactable currInteractable;

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
        UpdateInteractions();

        if (currInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currInteractable.Use();
        }

        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector2(inputH, inputV).normalized;

        if (inputDir != Vector3.zero)
        {
            animator2D.Play(moveAnim, true);
        }
        else
        {
            animator2D.Play(idleAnim, true);
        }

        spriteRenderer.flipX = inputH > 0;

        rigidbody2D.velocity = inputDir * moveSpeed;
    }

    private void UpdateInteractions()
    {
        if (currInteractable != null)
        {
            currInteractable.SetInteractionActive(false);
            currInteractable = null;
        }

        Collider2D col = Physics2D.OverlapCircle(transform.position, interactDist, interactLayer);
        if (col != null && col.TryGetComponent<Interactable>(out Interactable target))
        {
            currInteractable = target;
            currInteractable.SetInteractionActive(true);
        }
    }
}
