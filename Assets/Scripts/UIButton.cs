using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//[RequireComponent(typeof (AudioSource))]
public class UIButton : MonoBehaviour
{
    private AudioSource _source;
    public GameObject txt;
    public GameObject _border;
    public CanvasGroup group;
    public RectTransform parentRect;
    public Ease inEaseType;
    public Ease outEaseType;
    public GameObject[] images;
    public float duration;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (this.gameObject.TryGetComponent<Button>(out Button _button) && txt != null)
        {
            if (!_button.interactable)
            {
                txt.SetActive(false);
                GetComponent<Image>().raycastTarget = false;
            }

            else
            {
                GetComponent<Image>().raycastTarget = true;
                txt.SetActive(true);

            }

        }
    }
    //private void OnMouseEnter()
    //{
    //    if (this.gameObject.TryGetComponent<Button>(out Button _button))
    //    {
    //        if (_button.interactable)
    //        {
    //            if (_border!=null)
    //            {
    //                Debug.Log("true");
    //                _border.SetActive(true);
    //            }
    //        }
    //    }
    //}
    //private void OnMouseExit()
    //{

    //    if (_border != null)
    //    {
    //        _border.SetActive(false);
    //    }

    //}

   public void ClickAnimation()
    {

        this.transform.DOScale(new Vector3(0.95f,0.95f,0.95f),0.15f).OnComplete(()=>transform.DOScale(Vector3.one,0.15f)).SetEase(Ease.OutBounce);
    }

    public void PlaySound(AudioClip clip) =>_source.PlayOneShot(clip);

    public void PlayAudio(string name) => FindObjectOfType<AudioManager>().Play(name); 

    public void AnimateSideBar()
    {
        if (!isActive)
        {
            group.transform.gameObject.SetActive(true);
            isActive = true;
            DOVirtual.Float(0f, 1f, duration, (value) => group.alpha = value).SetEase(inEaseType);
            parentRect.DOAnchorPosY(-31.093f, duration).SetEase(inEaseType);

        }
        else if (isActive)
        {
            isActive = false;
            parentRect.DOAnchorPosY(16.111f, duration).SetEase(inEaseType);
            DOVirtual.Float(1f, 0f, duration, (value) => group.alpha = value).SetEase(outEaseType).OnComplete(() => {
                group.transform.gameObject.SetActive(false);
                foreach (var image in images)
                {
                    image.SetActive(false);
                }                                              }) ;

        }
    }
}
