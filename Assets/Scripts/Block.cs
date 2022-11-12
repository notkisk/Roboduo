using UnityEngine;

public class Block : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public LayerMask whatIsGrounde;
    public float groundCheckLineLength;
    public float groundCheckLineGap;
    public Transform bottomPoint;
    bool isGroundedLastFrame;
    private Rigidbody2D _rb;
    public float yOffset;
    public GameObject landEffect;
    GameObject _geralt;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void LateUpdate()
    {
        isGroundedLastFrame = Grounded();
    }

    private void Update()
    {
        _geralt = GameObject.FindGameObjectWithTag("Geralt");
        //if (isPlayerUnder())
        //{
        //    Kill();
        //}
        //if (Grounded())
        //{
        //    if (_rb.bodyType != RigidbodyType2D.Static)
        //    {
        //        _rb.velocity = new Vector2(_rb.velocity.x, 0f);

        //    }

        //}
        //else
        //{
        //    if (_rb.bodyType != RigidbodyType2D.Static)
        //    {
        //        if (_rb.velocity.y < 0f)
        //        {
        //            _rb.velocity = new Vector2(0f, _rb.velocity.y);
        //        }
        //    }

        //}
        if (_geralt!=null)
        {
            if (Vector2.Distance(this.transform.position, _geralt.transform.position) > 1.5f)
            {
                if (!Grounded())
                {
                    if (_rb.velocity.y < 1f)
                    {
                        _rb.velocity = new Vector2(0f, _rb.velocity.y);
                    }
                }
            }
        }
     
       
            if (!isGroundedLastFrame)
            {
                if (Grounded())
                    {
                        Debug.Log("Landed");
                        FindObjectOfType<AudioManager>().Play("BlockLand");
                        Instantiate(landEffect, new Vector3(bottomPoint.position.x, bottomPoint.position.y + yOffset, 0f), Quaternion.identity);
                
                    }
            }
        

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Elevator"))
        {
            this.transform.parent = collider.transform;
        }
        if (collider.gameObject.CompareTag("Robo"))
        {
            if (_geralt != null)
            {
                if (Vector2.Distance(this.transform.position, _geralt.transform.position) > 0.75f)
                {
                    _rb.mass = 1800f;
                }
            }
            else if(_geralt ==null)
            {
                _rb.mass = 1800f;

            }

        }


    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Elevator"))
        {
            this.transform.parent = null;

        }
       
        if (collider.gameObject.CompareTag("Robo"))
        {
            _rb.mass = 18f;


        }

    }

    bool Grounded()
    {
        Vector2 lineStart = new Vector2(bottomPoint.position.x, bottomPoint.position.y + groundCheckLineGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y + groundCheckLineLength);

        return Physics2D.Linecast(lineStart, lineEnd, whatIsGrounde);

    }

    bool isPlayerUnder()
    {
        Vector2 lineStart = new Vector2(bottomPoint.position.x, bottomPoint.position.y + groundCheckLineGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y + groundCheckLineLength);

        return Physics2D.Linecast(lineStart, lineEnd, whatIsPlayer);


    }

    void Kill()
    {
        StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1f, 1f));
    }

    private void OnDrawGizmos()
    {
        Vector2 lineStart = new Vector2(bottomPoint.position.x, bottomPoint.position.y + groundCheckLineGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y + groundCheckLineLength);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}
