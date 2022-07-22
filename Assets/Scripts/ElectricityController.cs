using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class ElectricityController : MonoBehaviour
{
    public enum State { Idle,Electricity}
    [HideInInspector]public State currentState;
    public GameObject yellowSparkels;
    public float electricityDuration;
    public int yellowSparklesEmmit;

    const string ELECTRICITY_ANIMATION_NAME = "Electricity";
    private Animator _anim;
    public Vector2 checkSize;
    public LayerMask blockLayer;
    public Vector2 checkOffset;
    private PlayerController _mycontroller;

    public bool electricising;
    Collider2D _collidingObject;
    Transform closestBlock;


    [Header("Camera Shake")]
    public float magnitude;
    public float roughness;
    public float fadeInTime;
    public float fadeOutTime;


    GameObject []blocks;
    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Idle;
        _anim = GetComponent<Animator>();
        _mycontroller = GetComponent<PlayerController>();

    }
    private void Awake()
    {
        blocks = GameObject.FindGameObjectsWithTag("ElectricityBlock");

    }

    // Update is called once per frame
    void Update()
    {
        //_collidingObject = CollidingObject();
          blocks = GameObject.FindGameObjectsWithTag("ElectricityBlock");
         closestBlock= GetClosestBlock(blocks);

        if (Input.GetKeyDown(KeyCode.X) && !_anim.GetCurrentAnimatorStateInfo(0).IsName(ELECTRICITY_ANIMATION_NAME))
        {
            
            Electricize(/*_collidingObject,*/blocks);
        }

    }

    private void Electricize(/*Collider2D coll,*/GameObject[]blocks)
    {
      
        if (_mycontroller.Grounded())
        {
            _anim.SetBool("isElectricity", true);
            FindObjectOfType<AudioManager>().Play("Electricity");
            StartCoroutine(ResetElecetricityAnimation());
            StartCoroutine(YellowSparklesEmmiter());
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            //if (coll!=null)
            //{
            //    if (_collidingObject.CompareTag("ElectricityBlock"))
            //    {
            //        if (currentState == State.Electricity)
            //        {
            //            coll.GetComponent<ElectricityBlock>().Trigger();
            //        }
            //    }

            //}
            closestBlock.GetComponent<ElectricityBlock>().Trigger();


        }
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //  _collidingObject = collision;
        
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    _collidingObject = collision;

    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    _collidingObject = null;

    //}
    IEnumerator YellowSparklesEmmiter()
    {
        for (int i = 0; i < yellowSparklesEmmit; i++)
        {
            yield return new WaitForSeconds(electricityDuration / yellowSparklesEmmit);
            Vector3 position = this.transform.position + Random.insideUnitSphere;
            GameObject _yellowSparkels = Instantiate(yellowSparkels, position, Quaternion.identity);
            Destroy(_yellowSparkels, 0.3f);
        }

    }

    IEnumerator ResetElecetricityAnimation()
    {
        electricising = true;
        _mycontroller.canMove = false;
        currentState = State.Electricity;
        GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(electricityDuration);
        _anim.SetBool("isElectricity", false);

        currentState = State.Idle;
        electricising = false;

        _mycontroller.canMove = true;
    }

    //private Collider2D CollidingObject()
    //{
    //    Physics2D.queriesStartInColliders = false;
    //    return Physics2D.BoxCast((Vector2)transform.position+ checkOffset, checkSize,0f,Vector2.zero,2f,blockLayer,Mathf.NegativeInfinity,Mathf.Infinity).collider;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube((Vector2)transform.position+ checkOffset, checkSize);
    }

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
}
