using UnityEngine;

public class PushingBlocks : MonoBehaviour
{
    private Animator _anim;
    public LayerMask whatIsBlock;
    public Transform handPosition;
    public float blockChecklineLength;
    public float blockCheckGap;

    public AudioClip pushingClip;
    bool isBlockAhead;

    PlayerController _myController;

    bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _myController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        isBlockAhead = BlockAhead();

        if (isBlockAhead && !Mathf.Approximately(_myController._horizontalInput,0f))
        {
            if (!isPlaying)
            {
                if (!Mathf.Approximately(Mathf.Abs (_myController._rb.velocity.x),0.25f))
                {
                    InvokeRepeating("PlayPushingLoop", 0f, pushingClip.length);
                    isPlaying = true;
                }

            }
            _anim.SetBool("isPushing", true);

        }
        else
        {
            FindObjectOfType<AudioManager>().StopPlaying("Pushing");
            isPlaying = false;
            _anim.SetBool("isPushing", false);
            if (IsInvoking())
            {
                CancelInvoke();
            }
        }
    }

    private bool BlockAhead()
    {
        Vector2 lineStart = new Vector2(handPosition.position.x+blockCheckGap,handPosition.position.y);
        Vector2 lineEnd = new Vector2(lineStart.x+ blockChecklineLength,lineStart.y);
        return Physics2D.Linecast(lineStart, lineEnd, whatIsBlock);
    }

    private void OnDrawGizmos()
    {
        Vector2 lineStart = new Vector2(handPosition.position.x + blockCheckGap, handPosition.position.y);
        Vector2 lineEnd = new Vector2(lineStart.x + blockChecklineLength, lineStart.y);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lineStart,lineEnd);
    }

    void PlayPushingLoop()
    {

        FindObjectOfType<AudioManager>().Play("Pushing");

    }
}
