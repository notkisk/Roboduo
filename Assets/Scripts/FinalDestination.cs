using UnityEngine;
using DG.Tweening;
public class FinalDestination : MonoBehaviour
{
    public float minDistance;
    GameObject _robo, _geralt;
    bool hasWon;
    public GameObject confetti;
    public GameObject downArrowKey;
    // Start is called before the first frame update
    void Start()
    {
        hasWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        _robo = GameObject.FindGameObjectWithTag("Robo");
        _geralt = GameObject.FindGameObjectWithTag("Geralt");
        if (_robo == null && _geralt == null && !hasWon)
        {
            hasWon = true;
            FindObjectOfType<AudioManager>().Play("Victory");

            Instantiate(confetti, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f), Quaternion.identity);
            StartCoroutine(FindObjectOfType<SceneController>().LoadNextScene(1f, 1f));
            return;
        }

        if (Vector2.Distance(this.transform.position,_robo.transform.position)<=minDistance||Vector2.Distance(this.transform.position,_geralt.transform.position)<=minDistance)
        {
            downArrowKey.SetActive(true);
        }
        else
        {
            downArrowKey.SetActive(false);

        }
     

        ////if (_robo == null || _geralt == null && !hasReloaded)
        ////{
        ////    hasReloaded = true;
        ////    StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1f, 0.5f));
        ////}
        //if (_robo != null)
        //{
        //    if (Vector2.Distance(this.transform.position, _geralt.transform.position) < minDistance)
        //    {
        //        if (Input.GetKeyDown(KeyCode.DownArrow))
        //        {
        //            //downArrow.SetActive(true);
        //            if (FindObjectOfType<PlayerSwitchController>().geraltController.isActiveAndEnabled)
        //            {
        //                FindObjectOfType<AudioManager>().Play("Switch");
        //                FindObjectOfType<PlayerSwitchController>().geraltController.Disable();
        //                FindObjectOfType<PlayerSwitchController>().roboController.Enable();
        //            }

        //            _geralt.transform.DOMove(this.transform.position, 0.5f);
        //            _geralt.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(_geralt.transform.root.gameObject));

        //        }
        //    }

        //}
        //else if (_geralt != null)
        //{
        //    if (Vector2.Distance(this.transform.position, _robo.transform.position) < minDistance)
        //    {
        //        if (Input.GetKeyDown(KeyCode.DownArrow))
        //        {
        //            //downArrow.SetActive(true);
        //            if (FindObjectOfType<PlayerSwitchController>().roboController.isActiveAndEnabled)
        //            {
        //                FindObjectOfType<AudioManager>().Play("Switch");
        //                FindObjectOfType<PlayerSwitchController>().geraltController.Enable();
        //                FindObjectOfType<PlayerSwitchController>().roboController.Disable();
        //            }

        //            _robo.transform.DOMove(this.transform.position, 0.5f);
        //            _robo.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(_robo.transform.root.gameObject));

        //        }
        //    }
        //}




    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Robo"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.DownArrow))
    //        {
    //            Debug.Log("Robo");
    //        }
    //    }
    //    else if (collision.gameObject.CompareTag("Geralt"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.DownArrow))
    //        {
    //            Debug.Log("Geralt");
    //        }
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Robo")||collision.gameObject.CompareTag("Geralt"))
    //    {
    //        if (collision.GetComponentInChildren<PlayerController>().isActiveAndEnabled)
    //        {
    //            if (Input.GetKeyDown(KeyCode.DownArrow))
    //            {
    //                Debug.Log($"{collision.gameObject.name} has entered the final destination");
    //            }
    //        }

    //    }
    //}
}
