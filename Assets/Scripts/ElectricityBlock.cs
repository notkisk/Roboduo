using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBlock : MonoBehaviour
{
    public float minimumDistance= 1f;
    bool isInTheRightPlace;
    public Elevator[] elevators;

    GameObject[] bases;
    GameObject _robo;

    public Sprite[] blockStates;
    SpriteRenderer _myRenderer;

    private void Start()
    {
        _myRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //Debug.Log($"isInTheRightPlace={isInTheRightPlace}");
        _robo = GameObject.FindGameObjectWithTag("Robo");
        bases = GameObject.FindGameObjectsWithTag("ElectricityBase");
        Transform closest= GetClosestBlock(bases);
       
            if (Vector2.Distance(this.transform.position, closest.transform.position) < minimumDistance)
            {
                isInTheRightPlace = true;
            }
            else
            {
                isInTheRightPlace = false;

            }
        
    }
    public void Trigger()
    {
        //add if statement fpr checking if we are in the right plce
        //trigger elevator
        //Debug.Log("Triggerd");
        if (!elevators[0].isElevating)
        {
            if (Vector2.Distance(this.transform.position, _robo.transform.position) < minimumDistance)
            {
                if (isInTheRightPlace)
                {

                    StartCoroutine(ChangeState());
                    foreach (var elevator in elevators)
                    {
                        if (!elevator.isElevating)
                        {
                            elevator.Elevate();
                        }
                    }
                }
            }
        }
        
        
       
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{

    //    if (collision.gameObject.CompareTag("ElectricityBase"))
    //    {
    //        isInTheRightPlace = true;
    //    }

    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("ElectricityBase"))
    //    {
    //        isInTheRightPlace = false;
    //    }
    //}

    Transform GetClosestBlock(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.transform;
    }

    IEnumerator ChangeState()
    {

        _myRenderer.sprite = blockStates[1];
        yield return new WaitForSeconds(elevators[0].elevateSpeed + elevators[0].delay);
        _myRenderer.sprite = blockStates[0];

    }
}
