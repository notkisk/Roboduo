using UnityEngine;

public class SetTheParentToElevator : MonoBehaviour
{
 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block")||collision.gameObject.CompareTag("ElectricityBlock")||collision.gameObject.CompareTag("Geralt") || collision.gameObject.CompareTag("Robo"))
        {
            collision.transform.parent = this.transform;
            //this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("ElectricityBlock") || collision.gameObject.CompareTag("Geralt") || collision.gameObject.CompareTag("Robo"))
        {
            collision.transform.parent = null;
            //this.transform.parent = null;
        }
    }
}
