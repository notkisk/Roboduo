using UnityEngine;

public class PushingBlocks : MonoBehaviour
{
    private Animator _anim;
    public LayerMask whatIsBlock;
    public Transform handPosition;
    public float blockChecklineLength;
    public float blockCheckGap;


    bool isBlockAhead;

    PlayerController _myController;
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
            _anim.SetBool("isPushing",true);
        }
        else
        {
            _anim.SetBool("isPushing", false);

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
}
