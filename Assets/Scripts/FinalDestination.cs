using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FinalDestination : MonoBehaviour
{
    public float minDistance;
    GameObject _robo, _geralt;
    bool hasWon;
    public GameObject confetti;
    public GameObject downArrowKey;

    int nextSceneLoad;
    private bool hasReloaded=false;

    // Start is called before the first frame update
    void Start()
    {
        hasWon = false;
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        _robo = GameObject.FindGameObjectWithTag("Robo");
        _geralt = GameObject.FindGameObjectWithTag("Geralt");
        
        //if (_robo == null || _geralt == null && !hasReloaded && FindObjectOfType<FinishingTheLevel>().stupedWayToKnowIfWeWon < 1)
        //{
        //    hasReloaded = true;
        //    StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1f, 0.5f));
        //    return;
        //}
        if (GameObject.Find("key")!=null)
        {
            if (FindObjectOfType<KeyLockSystem>().isTriggerd)
            {
                if (_robo == null && _geralt == null && !hasWon)
                {
                    hasWon = true;
                    FindObjectOfType<AudioManager>().Play("Victory");
                   
                    Instantiate(confetti, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f), Quaternion.identity);
                    if (SceneManager.GetActiveScene().buildIndex !=18)
                    {

                        StartCoroutine(FindObjectOfType<SceneController>().LoadNextScene(1f, 1f));

                    }
                    if (SceneManager.GetActiveScene().buildIndex == 18)
                    {
                        StartCoroutine(FindObjectOfType<SceneController>().LoadScene(2f,0));

                    }
                    else
                    {

                        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                        {
                            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                        }

                    }

                }

                if (Vector2.Distance(this.transform.position, _robo.transform.position) <= minDistance || Vector2.Distance(this.transform.position, _geralt.transform.position) <= minDistance)
                {
                    downArrowKey.SetActive(true);
                }
                else
                {
                    downArrowKey.SetActive(false);
                }
            }

        }
        else
        {
            if (_robo == null && _geralt == null && !hasWon)
            {
                hasWon = true;
                FindObjectOfType<AudioManager>().Play("Victory");
               
                Instantiate(confetti, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f), Quaternion.identity);
                if (SceneManager.GetActiveScene().buildIndex != 18)
                {
                    StartCoroutine(FindObjectOfType<SceneController>().LoadNextScene(1f, 1f));

                }
                if (SceneManager.GetActiveScene().buildIndex == 18)
                {
                    StartCoroutine(FindObjectOfType<SceneController>().LoadScene(2f, 0));
                }
                else
                {

                    if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                    {
                        PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                    }

                }
            }

            if (_robo == null || _geralt == null && FindObjectOfType<FinishingTheLevel>().stupedWayToKnowIfWeWon < 1) return;
            if (_robo!=null&&_geralt!=null)
            {
                if (Vector2.Distance(this.transform.position, _robo.transform.position) <= minDistance || Vector2.Distance(this.transform.position, _geralt.transform.position) <= minDistance)
                {
                    downArrowKey.SetActive(true);
                }
                else
                {
                    downArrowKey.SetActive(false);
                }
            }
         

          

        }


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

    public void ReloadSceneTrick()
    {

      StartCoroutine ( FindObjectOfType<SceneController>().ReloadScene(1f,1f));
    }
}
