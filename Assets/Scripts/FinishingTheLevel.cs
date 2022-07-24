using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishingTheLevel : MonoBehaviour
{
    public PlayerController myController;
    GameObject finishDoor;
    int stupedWayToKnowIfWeWon;


    // Start is called before the first frame update
    void Start()
    {
        stupedWayToKnowIfWeWon = 0;
        finishDoor = GameObject.FindGameObjectWithTag("Finish");
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject _robo = GameObject.Find("Robo");
        GameObject _geralt = GameObject.Find("Geralt");

        if (GameObject.Find("key") !=null)
        {
            if (FindObjectOfType<KeyLockSystem>().isTriggerd==true)
            {
                if (Vector2.Distance(this.transform.position, finishDoor.transform.position) < 1f)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (FindObjectOfType<PlayerSwitchController>().activePlayer == myController)
                        {
                            FindObjectOfType<AudioManager>().Play("Finish");
                            if (this.gameObject.CompareTag("Robo"))
                            {
                                if (_robo != null && _geralt != null)
                                {
                                    FindObjectOfType<PlayerSwitchController>().roboController.Disable();
                                    FindObjectOfType<PlayerSwitchController>().geraltController.Enable();
                                    FindObjectOfType<PlayerSwitchController>().activePlayer = FindObjectOfType<PlayerSwitchController>().geraltController;
                                }
                                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
                                transform.DOMove(finishDoor.transform.position, 0.5f);
                            }
                            if (this.gameObject.CompareTag("Geralt"))
                            {
                                if (_robo != null && _geralt != null)
                                {
                                    FindObjectOfType<PlayerSwitchController>().roboController.Enable();
                                    FindObjectOfType<PlayerSwitchController>().geraltController.Disable();
                                    FindObjectOfType<PlayerSwitchController>().activePlayer = FindObjectOfType<PlayerSwitchController>().roboController;

                                }

                                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
                                transform.DOMove(finishDoor.transform.position, 0.5f);
                            }
                            stupedWayToKnowIfWeWon++;

                        }

                    }
                }
            }
        }
        else
        {
            if (Vector2.Distance(this.transform.position, finishDoor.transform.position) < 1f)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (FindObjectOfType<PlayerSwitchController>().activePlayer == myController)
                    {
                        FindObjectOfType<AudioManager>().Play("Finish");
                        if (this.gameObject.CompareTag("Robo"))
                        {
                            if (_robo != null && _geralt != null)
                            {
                                FindObjectOfType<PlayerSwitchController>().roboController.Disable();
                                FindObjectOfType<PlayerSwitchController>().geraltController.Enable();
                                FindObjectOfType<PlayerSwitchController>().activePlayer = FindObjectOfType<PlayerSwitchController>().geraltController;
                            }
                            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
                            transform.DOMove(finishDoor.transform.position, 0.5f);
                        }
                        if (this.gameObject.CompareTag("Geralt"))
                        {
                            if (_robo != null && _geralt != null)
                            {
                                FindObjectOfType<PlayerSwitchController>().roboController.Enable();
                                FindObjectOfType<PlayerSwitchController>().geraltController.Disable();
                                FindObjectOfType<PlayerSwitchController>().activePlayer = FindObjectOfType<PlayerSwitchController>().roboController;

                            }

                            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
                            transform.DOMove(finishDoor.transform.position, 0.5f);
                        }
                        stupedWayToKnowIfWeWon++;

                    }

                }
            }
        }
       
       
        
    }

   
   
}
