using UnityEngine;
using DG.Tweening;

public class KeyLockSystem : MonoBehaviour
{
    public float minimumDistance;
    public float animationSpeed;
    public Ease easeType;
    public GameObject _lock;


    GameObject _robo, _geralt;

    [HideInInspector] public bool isTriggerd;
    private void Start()
    {
        isTriggerd = false;
        transform.DOMoveY(transform.position.y + 0.25f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Unset);

    }

    void Update()
    {
        _robo = GameObject.FindGameObjectWithTag("Robo");
        _geralt = GameObject.FindGameObjectWithTag("Geralt");
        if (Vector2.Distance(this.transform.position,_robo.transform.position)<minimumDistance||Vector2.Distance(this.transform.position,_geralt.transform.position)<minimumDistance)
        {
            if (isTriggerd==false)
            {
                isTriggerd = true;
                FindObjectOfType<AudioManager>().Play("PickUp");
                this.transform.DOScale(Vector2.zero, animationSpeed).SetEase(easeType);
                transform.DORotate(new Vector3(0f, 0f, 30f), animationSpeed).SetEase(easeType);

                _lock.transform.DOScale(Vector2.zero, animationSpeed).SetEase(easeType);
                _lock.transform.DORotate(new Vector3(0f, 0f, 30f), animationSpeed).SetEase(easeType);
            }
  
        } 
    }
}
