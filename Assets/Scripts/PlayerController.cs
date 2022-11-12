using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    enum state { enabled,disabled };
    state currentState;
    [Header("Movement")]
    [HideInInspector]public Rigidbody2D _rb;
    public float maxMoveSpeed;
    public float _acceleration, _deacceleration;
    public Ease easeType = Ease.Linear;
   [HideInInspector] public float _horizontalInput;
    float currentMoveSpeed;
    [HideInInspector]public bool canMove = true;
    bool isFacingRight=true;
    [Header("Jumping")]
    public LayerMask whatIsGround;
    public Transform footPosition;
    public Transform headPosition;
    public float jumpForce;
    public float groundCheckLength;
    public float groundCheckGap = 0.025f;
    public float headCheckLength;
    public float headCheckGap = 0.025f;
    public float hangTime = 0.2f;
    public float groundCheckRadius;
    public float headCheckRadius;
    private bool isGrounded;
    private bool isCeiling;
    private bool groundedLastFrame;
    private float hangTimeCounter;
    private Animator _anim;
    public LayerMask whatIsBlock;


    [Header("Effects")]
    public GameObject jumpingEffect;
    public GameObject landingEffect;
    public Vector2 landingEffectOffset;
    public float footStepRate;
    public GameObject deathEffect;

    float timeBtwFootStep;

    public Color _disabledColor;
    private CapsuleCollider2D _myCapsuleCollider;
    private BoxCollider2D _myBoxCollider;
    private SpriteRenderer _myRenderer;
    Rigidbody2D _parentBody;

    Color _startingColor;

    private static readonly int Walk = Animator.StringToHash("isWalking");
    public bool isElectricityLevel;
    private void Awake()
    {
        timeBtwFootStep = footStepRate;
        _rb = GetComponentInParent<Rigidbody2D>();
        _myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        _parentBody = GetComponentInParent<Rigidbody2D>();
        _myRenderer = GetComponent<SpriteRenderer>();
        _startingColor = _myRenderer.color;
        _myBoxCollider = GetComponent<BoxCollider2D>();
        
        _anim = GetComponent<Animator>();
   

    }

    // Update is called once per frame
    private void LateUpdate()
    {
        groundedLastFrame = isGrounded;
    }
    void Update()
    {
        if (currentState == state.disabled)
        {
            _rb.velocity = Vector2.zero;

        }
        //transform.parent.position = transform.TransformVector(this.transform.position);
        _horizontalInput =Input.GetAxisRaw("Horizontal");
        isGrounded = Grounded();
        isCeiling = Ceiling();
        //Debug.Log($"isGrounded = {isGrounded}  isCeiling = {isCeiling}");

        if (isGrounded)
        {
            hangTimeCounter = hangTime;
            _anim.SetBool("isJumping", false);
            _anim.SetBool("isFalling",false);
        }
        else
        {
            hangTimeCounter -= Time.deltaTime;
        }
        FlipHandler();
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Electricity"))
        {
            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Z))
            {
                if (hangTimeCounter > 0f)
                {
                    FindObjectOfType<AudioManager>().Play("Jump");

                    Instantiate(jumpingEffect, footPosition.position, Quaternion.identity);
                    _rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
                    _anim.SetBool("isJumping", true);
                    transform.parent.DOScale(new Vector3(transform.parent.localScale.x - 0.25f, transform.parent.localScale.y + 0.25f, 1f), 0.1f).SetEase(Ease.Linear).OnComplete(() => transform.parent.DOScale(Vector3.one, 0.1f).SetEase(Ease.Linear));

                }
            }
        }
        
        if (Mathf.Approximately(_horizontalInput,0f))
        {
            _anim.SetBool("isWalking", false);

        }
        if (_rb.velocity.y < 0f&& !isGrounded)
        {
            _anim.SetBool("isJumping", false);
            _anim.SetBool("isFalling", true);

        }
        else if (Mathf.Approximately(_rb.velocity.y,0f))
        {
            _anim.SetBool("isFalling", false);

        }

        if (!groundedLastFrame)
            {
                if (isGrounded)
                {
                    _anim.SetBool("isFalling", false);

                if (_rb.velocity.y < 0f)
                    {

                        Instantiate(landingEffect, (Vector2)footPosition.position + landingEffectOffset, Quaternion.identity);
                                  FindObjectOfType<AudioManager>().Play("Land");
                        //_anim.SetTrigger("isLanding");
                        transform.parent.DOScale(new Vector3(1.1f, 0.9f, 1f), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                        transform.parent.DOScale(Vector3.one, 0.1f).SetEase(Ease.Linear));

                    }
                }
             

            }
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Geralt_Jump"))
        {
            if (_rb.velocity.y<0f)
            {
                _anim.SetBool("isFalling", true);
            }
        }

        //if (!Input.GetButtonDown("Jump"))
        //{
        //    if (groundedLastFrame && !isGrounded)
        //    {
        //        if (_rb.velocity.y < 0f)
        //        {
        //            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Geralt_Falling"))
        //            {
        //                _anim.SetBool("irregularFalling", true);
        //            }
        //        }
        //    }
        //}
        
        if (canMove && !_anim.GetCurrentAnimatorStateInfo(0).IsName("Pushing"))
        {
            if (isGrounded && !Mathf.Approximately(_horizontalInput, 0f) && timeBtwFootStep <= 0f)
            {
                timeBtwFootStep = footStepRate;
                FindObjectOfType<AudioManager>().Play("Footstep");
            }
            else
            {
                timeBtwFootStep -= Time.deltaTime;
            }
        }

        if (!isGrounded)
        {
            if (_rb.velocity.y<0f)
            {
                if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Geralt_Falling"))
                {
                    _anim.SetBool("irregularFalling", true);
                }
            }
        }
        else
        {
            _anim.SetBool("irregularFalling", false);

            //_anim.SetBool("isFalling",false);
        }
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            DOVirtual.Float(currentMoveSpeed, _horizontalInput * maxMoveSpeed, _horizontalInput != 0f ? _acceleration : _deacceleration, value => { currentMoveSpeed = value; }).SetEase(easeType);
            _rb.velocity = new Vector2(currentMoveSpeed, _rb.velocity.y);
            if (isGrounded)
            {
                _anim.SetBool("isWalking", true);

            }
            else
            {
                _anim.SetBool("isWalking", false);

            }
        }

    }

   private void FlipHandler()
    {
        if (!isFacingRight&&_horizontalInput>0f)
        {
            Flip();
        }
        else if (isFacingRight&&Mathf.Sign(_horizontalInput)==-1f)
        {
            Flip();
        }

    }

   

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

    }


    public bool Grounded()
    {
        Vector2 lineStart = new Vector2(footPosition.position.x, footPosition.position.y - groundCheckGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y - groundCheckLength);
        return Physics2D.OverlapCircle(lineEnd, groundCheckRadius, whatIsGround);
    }
    public bool Ceiling()
    {

        Vector2 lineStart = new Vector2(headPosition.position.x, headPosition.position.y);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y + headCheckLength);
        return Physics2D.Linecast(lineStart, lineEnd, whatIsBlock);

    }
 
    private void OnDrawGizmos()
    {
        
        Vector2 lineStart = new Vector2(footPosition.position.x, footPosition.position.y - groundCheckGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y - groundCheckLength);
        Vector2 headlineStart = new Vector2(headPosition.position.x, headPosition.position.y);

        Vector2 headLineEnd = new Vector2(headPosition.position.x, headPosition.position.y + headCheckLength);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lineEnd, groundCheckRadius);
        Gizmos.DrawLine(headlineStart, headLineEnd);

    }

    public void Disable()
    {
        currentState = state.disabled;

        //GetComponentInParent<PlayerSwitchController>().enabled = false;
        _rb.velocity = Vector2.zero;
        _anim.SetBool("isWalking",false);
        //_myBoxCollider.enabled = false;
        //_myCapsuleCollider.enabled = false;
        _myRenderer.DOColor(_disabledColor, 0.25f);
        this.enabled = false;
        //_parentBody.bodyType = RigidbodyType2D.Static;

        if (TryGetComponent<ElectricityController>(out ElectricityController _electricityController))
        {
            if (isElectricityLevel)
            {
                _electricityController.enabled = false;
            }
        }
        else if (TryGetComponent<PushingBlocks>(out PushingBlocks _pushingController))
        {
            _pushingController.enabled = false;
        }

    }

    public void Enable()
    {
        _rb.velocity = Vector2.zero;
        currentState = state.enabled;
        //GetComponentInParent<PlayerSwitchController>().enabled = true;
        _parentBody.bodyType = RigidbodyType2D.Dynamic;
        _myBoxCollider.enabled = true;
        //_myCapsuleCollider.enabled = true;
        _myRenderer.DOColor(_startingColor, 0.25f);
        this.enabled = true;
        if (TryGetComponent<ElectricityController>(out ElectricityController _electricityController))
        {
            if (isElectricityLevel)
            {
                _electricityController.enabled = true;
            }
        }
        else if (TryGetComponent<PushingBlocks>(out PushingBlocks _pushingController))
        {
            _pushingController.enabled = true;
        }
    }

   

}
