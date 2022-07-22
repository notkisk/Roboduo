using UnityEngine;

public class SetTheParentToElevator : MonoBehaviour
{
 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Elevator"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Elevator"))
        {
            this.transform.parent =null;

        }
    }
}
