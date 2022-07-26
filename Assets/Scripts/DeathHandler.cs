using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DeathHandler : MonoBehaviour
{
    public Transform footPosition,headPosition;
    public float groundCheckGap, groundCheckLength,groundCheckRadius, headCheckLength;
    public LayerMask whatIsGround, whatIsBlock;
    public GameObject deathEffect;
    public event Action PlayerDied;
    private void OnDestroy()
    {
        FindObjectOfType<FinishingTheLevel>().stupedWayToKnowIfWeWon++;
    }
    private void Update()
    {
        var isGrounded = Grounded();
        var isCeiling = Ceiling();
        if (isGrounded && isCeiling)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            //PlayerDied?.Invoke();
            FindObjectOfType<FinalDestination>().ReloadSceneTrick();

            //if (this.gameObject.tag==("Robo"))
            //{
            //    StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1f, 1f));
            //    FindObjectOfType<AudioManager>().Play("RoboDeath");
            //    //Instantiate(deathFX, collision.transform.position, Quaternion.identity);
            //    //collision.transform.parent.DOScale(Vector3.zero, 0.2f).OnComplete(() => Destroy(collision.transform.root.gameObject));
            //    FindObjectOfType<AudioManager>().Play("Switch");
            //    FindObjectOfType<PlayerSwitchController>().geraltController.Enable();
            //    FindObjectOfType<PlayerSwitchController>().roboController.Disable();
            //    FindObjectOfType<PlayerSwitchController>().activePlayer = FindObjectOfType<PlayerSwitchController>().geraltController;
            //    ;
            //    //Destroy(collision.transform.root.gameObject);

            //}
            //if (this.gameObject.tag==("Geralt"))
            //{
            //    StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1f, 1f));

            //    FindObjectOfType<AudioManager>().Play("GeraltDeath");
            //    //Instantiate(deathFX, collision.transform.position, Quaternion.identity);
            //    //collision.transform.parent.DOScale(Vector3.zero, 0.2f).OnComplete(() => Destroy(collision.transform.root.gameObject));
            //    FindObjectOfType<AudioManager>().Play("Switch");
            //    FindObjectOfType<PlayerSwitchController>().geraltController.Disable();
            //    FindObjectOfType<PlayerSwitchController>().roboController.Enable();
            //    FindObjectOfType<PlayerSwitchController>().activePlayer = FindObjectOfType<PlayerSwitchController>().roboController;
            //    //Destroy(collision.transform.root.gameObject);


            //}

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

    private void OnDrawGizmos()
    {
        Vector2 lineStart = new Vector2(headPosition.position.x, headPosition.position.y);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y + headCheckLength);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}
