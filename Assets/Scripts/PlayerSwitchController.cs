using UnityEngine;

public class PlayerSwitchController : MonoBehaviour
{
     public PlayerController roboController;
    public PlayerController geraltController;
    public bool isRobo, isGeralt;

    public PlayerController activePlayer;
    // Start is called before the first frame update
    void Start()
    {
        //roboController = GameObject.FindGameObjectWithTag("Robo").GetComponent<PlayerController>();
        //geraltController = GameObject.FindGameObjectWithTag("Geralt").GetComponent<PlayerController>();

        if (geraltController.isActiveAndEnabled && roboController.isActiveAndEnabled)
        {
            if (isRobo)
            {
                geraltController.Disable();
                roboController.Enable();
                activePlayer = roboController;
            }
            else if (isGeralt)
            {
                geraltController.Enable();
                roboController.Disable();
                activePlayer = geraltController;

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (roboController!=null&& geraltController!=null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {

                if (geraltController.isActiveAndEnabled )
                {
                    if (geraltController.Grounded())
                    {
                        FindObjectOfType<AudioManager>().Play("Switch");
                        geraltController.Disable();
                        roboController.Enable();
                        activePlayer = roboController;


                    }

                }
                else if (roboController.isActiveAndEnabled)
                {
                    if (roboController.Grounded())
                    {
                        FindObjectOfType<AudioManager>().Play("Switch");

                        geraltController.Enable();
                        roboController.Disable();
                        activePlayer = geraltController;

                    }

                }


            }
        }
        if (activePlayer==null)
        {
            if (roboController.gameObject.activeInHierarchy)
            {
                activePlayer = roboController;
            }
            else if (geraltController.gameObject.activeInHierarchy)
            {
                activePlayer = geraltController;

            }
        }
      
        //if (activePlayer==null)
        //{
        //    if (GameObject.Find("Robo")==null)
        //    {
        //        if (geraltController.Grounded())
        //        {
        //            FindObjectOfType<AudioManager>().Play("Switch");
        //            geraltController.Disable();
        //            roboController.Enable();
        //            activePlayer = roboController;


        //        }

        //    }
        //    else if (GameObject.Find("Geralt") == null)
        //    {
        //        if (roboController.Grounded())
        //        {
        //            FindObjectOfType<AudioManager>().Play("Switch");

        //            geraltController.Enable();
        //            roboController.Disable();
        //            activePlayer = geraltController;

        //        }

        //    }
        //}


    }
}
