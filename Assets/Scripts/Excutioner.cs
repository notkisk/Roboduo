using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Excutioner : MonoBehaviour
{
    public GameObject deathFX;
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Robo"))
        {
            StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1f, 1f));
            FindObjectOfType<AudioManager>().Play("RoboDeath");
            Instantiate(deathFX, collision.transform.position, Quaternion.identity);
            //collision.transform.parent.DOScale(Vector3.zero, 0.2f).OnComplete(() => Destroy(collision.transform.root.gameObject));
            FindObjectOfType<AudioManager>().Play("Switch");
            FindObjectOfType<PlayerSwitchController>().geraltController.Enable();
            FindObjectOfType<PlayerSwitchController>().roboController.Disable();
            FindObjectOfType<PlayerSwitchController>().activePlayer = FindObjectOfType<PlayerSwitchController>().geraltController;
            ;
            Destroy(collision.transform.root.gameObject);

        }
        if (collision.gameObject.CompareTag("Geralt"))
        {
            StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1f, 1f));

            FindObjectOfType<AudioManager>().Play("GeraltDeath");
            Instantiate(deathFX, collision.transform.position, Quaternion.identity);
            //collision.transform.parent.DOScale(Vector3.zero, 0.2f).OnComplete(() => Destroy(collision.transform.root.gameObject));
            FindObjectOfType<AudioManager>().Play("Switch");
            FindObjectOfType<PlayerSwitchController>().geraltController.Disable();
            FindObjectOfType<PlayerSwitchController>().roboController.Enable();
            FindObjectOfType<PlayerSwitchController>().activePlayer = FindObjectOfType<PlayerSwitchController>().roboController;
            Destroy(collision.transform.root.gameObject);


        }
    }
}
