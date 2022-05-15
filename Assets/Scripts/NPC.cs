using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private PatrolPoint[] patrolPoints;
    [SerializeField] private float moveSpeed = 1f;

    private int currTarget = 0;
    private bool isIdle = false;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isIdle)
        {
            return;
        }

        PatrolPoint currPoint = patrolPoints[currTarget];
        Vector2 moveVector = currPoint.point.position - transform.position;

        if (moveVector.sqrMagnitude < 0.1f)
        {
            StartCoroutine(IdleTimer(currPoint.idleTime));
            currTarget = (currTarget + 1) % patrolPoints.Length;
            rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            rigidbody2D.velocity = moveVector.normalized * moveSpeed;
        }
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
