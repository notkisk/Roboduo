using UnityEngine;
using DG.Tweening;


public class Gate : MonoBehaviour
{

    public float moveAmount;
    public float openSpeed;
    public float closeSpeed;
    public Ease easeType;
    public bool x, y;

    Vector3 startingPosition;


    private void Start()
    {
        startingPosition = transform.position;
    }
    public void Open() {

        if (transform.position.y <= startingPosition.y + moveAmount)
        {
            if (y)
            {
           
                transform.DOMoveY(startingPosition.y + moveAmount, openSpeed).SetEase(easeType);

            }
        }
         if (x)
        {
             transform.DOMoveY(transform.position.x + moveAmount, openSpeed).SetEase(easeType);
        }

    }
    public void Close() {

        transform.DOMove(startingPosition, closeSpeed).SetEase(easeType);
    
    }

}
