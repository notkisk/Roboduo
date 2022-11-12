using UnityEngine;
using DG.Tweening;
using System.Collections;
public class Elevator : MonoBehaviour
{
    public float elevateSpeed;
    public float elevateAmount;
    public Ease easeType;
    [HideInInspector] public bool isElevating = false;
    public bool x, y;
    public AudioClip _elevatorSfx;
    public float delay;

    public void Elevate()
    {
        //Debug.Log("elevating");
        if (y)
        {
            StartCoroutine(PlayElevatorSfx());
            transform.DOMoveY(transform.position.y + elevateAmount, elevateSpeed).SetEase(easeType).OnComplete(() => {
                isElevating = false;
                elevateAmount *= -1f;
                FindObjectOfType<AudioManager>().StopPlaying("Elevator");
            }).SetDelay(delay);
            isElevating = true;
        }
        else if (x)
        {
            StartCoroutine(PlayElevatorSfx());
            transform.DOMoveX(transform.position.y + elevateAmount, elevateSpeed).SetEase(easeType).OnComplete(() => {
                isElevating = false;
                elevateAmount *= -1f;
                FindObjectOfType<AudioManager>().StopPlaying("Elevator");

            }).SetDelay(delay);
            isElevating = true;
        }
   

    }
    
    IEnumerator PlayElevatorSfx()
    {
        yield return new WaitForSeconds(delay);
        FindObjectOfType<AudioManager>().Play("Elevator");
    }

}
