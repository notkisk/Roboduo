using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public Transform footPosition,headPosition;
    public float groundCheckGap, groundCheckLength,groundCheckRadius, headCheckLength;
    public LayerMask whatIsGround, whatIsBlock;
    public GameObject deathEffect;

    private void Update()
    {
        var isGrounded = Grounded();
        var isCeiling = Ceiling();
        if (isGrounded && isCeiling)
        {
            StartCoroutine (FindObjectOfType<SceneController>().ReloadScene(1f, 1f));
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            if (this.gameObject.tag == "Robo")
            {
                FindObjectOfType<AudioManager>().Play("RoboDeath");
            }
            else if (this.gameObject.CompareTag("Geralt"))
            {
                FindObjectOfType<AudioManager>().Play("GeraltDeath");

            }
            Destroy(gameObject);
        }
    }
    public bool Grounded()
    {
        Vector2 lineStart = new Vector2(footPosition.position.x, footPosition.position.y - groundCheckGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y - groundCheckLength);
        return Physics2D.OverlapCircle(lineEnd, groundCheckRadius, whatIsGround);
    }
    public bool Ceiling()
    {

        Vector2 lineStart = new Vector2(headPosition.position.x, headPosition.position.y);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y + headCheckLength);
        return Physics2D.Linecast(lineStart, lineEnd, whatIsBlock);

    }
}
