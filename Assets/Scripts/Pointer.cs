using UnityEngine;
using DG.Tweening;

public class Pointer : MonoBehaviour
{
    public float tweenDuration;
    public Ease easeType;
    public float yOffset;
 

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerSwitchController>().activePlayer == null)
            return;

        Vector3 activePlayerPos = FindObjectOfType<PlayerSwitchController>().activePlayer.transform.position;
        Vector3 pointerPosition = new Vector3(activePlayerPos.x, activePlayerPos.y + yOffset, 0f);
        transform.DOMove(pointerPosition, tweenDuration).SetEase(easeType);
    }
}
