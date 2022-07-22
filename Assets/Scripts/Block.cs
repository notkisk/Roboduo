using UnityEngine;

public class Block : MonoBehaviour
{
    public LayerMask whatIsGrounde;
    public float groundCheckLineLength;
    public float groundCheckLineGap;
    public Transform bottomPoint;
    bool isGroundedLastFrame;
    private Rigidbody2D _rb;
    public float yOffset;
    public GameObject landEffect;
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
        if (Grounded())
        {
            if (_rb.bodyType != RigidbodyType2D.Static)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0f);

            }

        }
        else
        {
            if (_rb.bodyType != RigidbodyType2D.Static)
            {
                if (_rb.velocity.y < 0f)
                {
                    _rb.velocity = new Vector2(0f, _rb.velocity.y);
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
        if (collider.gameObject.CompareTag("Robo"))
        {
            _rb.bodyType = RigidbodyType2D.Static;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (collider.gameObject.CompareTag("Elevator"))
        {
            this.transform.parent = collider.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Robo"))
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;

            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        }
        if (collider.gameObject.CompareTag("Elevator"))
        {
            this.transform.parent = null;

        }
    }

    bool Grounded()
    {
        Vector2 lineStart = new Vector2(bottomPoint.position.x, bottomPoint.position.y + groundCheckLineGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y + groundCheckLineLength);

        return Physics2D.Linecast(lineStart, lineEnd, whatIsGrounde);

    }

    private void OnDrawGizmos()
    {
        Vector2 lineStart = new Vector2(bottomPoint.position.x, bottomPoint.position.y + groundCheckLineGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y + groundCheckLineLength);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}
