using UnityEngine;

public class InGameButton : MonoBehaviour
{
    SpriteRenderer _myRenderer;
    public Sprite[] sprites;

    public Gate[] gates;
    public float triggerMinDistance;

    bool blockAwayFromButton;

    GameObject /*_robo,*/ _geralt, _blocks, _Eblock;

    void Start()
    {
        _myRenderer = GetComponent<SpriteRenderer>();
         //_robo = GameObject.FindGameObjectWithTag("Robo");
         _geralt = GameObject.FindGameObjectWithTag("Geralt");
    
    }

    private void Update()
    {
        if (/*_robo!=null&& */_geralt!=null)
        {
            //_robo = GameObject.FindGameObjectWithTag("Robo");
            _geralt = GameObject.FindGameObjectWithTag("Geralt");

        }
        _blocks = GameObject.FindGameObjectWithTag("Block");
        _Eblock = GameObject.FindGameObjectWithTag("ElectricityBlock");

        //for (int i = 0; i < _blocks.Length; i++)
        //{
        //    if (i == _blocks.Length)
        //        i = 0;


        //    if (Vector2.Distance(transform.position, _blocks[i].transform.position) > triggerMinDistance)
        //    {
        //        blockAwayFromButton = true;
        //    }
        //    else
        //    {
        //        blockAwayFromButton = false;

        //    }
        //}
        if (_geralt!=null&&_blocks!=null&& _Eblock!=null)
        {
            if (Vector2.Distance(this.transform.position, _geralt.transform.position) > triggerMinDistance && Vector2.Distance(this.transform.position, _blocks.transform.position) > triggerMinDistance && Vector2.Distance(this.transform.position, _Eblock.transform.position) > triggerMinDistance)
            {
                _myRenderer.sprite = sprites[0];
                foreach (var gate in gates)
                {
                    gate.Close();
                }
            }
        }
        //else if( _geralt!=null)
        //{
        //    if ( Vector2.Distance(this.transform.position, _geralt.transform.position) > triggerMinDistance && Vector2.Distance(this.transform.position, _blocks.transform.position) > triggerMinDistance&& Vector2.Distance(this.transform.position, _Eblock.transform.position) > triggerMinDistance)
        //    {
        //        _myRenderer.sprite = sprites[0];
        //        foreach (var gate in gates)
        //        {
        //            gate.Close();
        //        }
        //    }
        //}
        //else
        //{
        //    _myRenderer.sprite = sprites[0];
        //    foreach (var gate in gates)
        //    {
        //        gate.Close();
        //    }
        //}
        //else if (_ _geralt == null)
        //{
        //    if (Vector2.Distance(this.transform.position, _robo.transform.position) > triggerMinDistance && Vector2.Distance(this.transform.position, _blocks.transform.position) > triggerMinDistance && Vector2.Distance(this.transform.position, _Eblock.transform.position) > triggerMinDistance)
        //    {
        //        _myRenderer.sprite = sprites[0];
        //        foreach (var gate in gates)
        //        {
        //            gate.Close();
        //        }
        //    }
        //}


        //else if (Vector2.Distance(this.transform.position, _robo.position) < triggerMinDistance || Vector2.Distance(this.transform.position, _geralt.position) < triggerMinDistance)
        //{
        //    _myRenderer.sprite = sprites[1];
        //    foreach (var gate in gates)
        //    {
        //        gate.Open();
        //    }
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ( collision.gameObject.CompareTag("Geralt")||collision.gameObject.CompareTag("Block")||collision.gameObject.CompareTag("ElectricityBlock"))
        {
            _myRenderer.sprite = sprites[1];
            foreach (var gate in gates)
            {
                gate.Open();
                FindObjectOfType<AudioManager>().Play("InGameButton");
            }
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Robo") || collision.gameObject.CompareTag("Geralt"))
    //    {
    //        _myRenderer.sprite = sprites[0];
    //        foreach (var gate in gates)
    //        {
    //            gate.Close();
    //        }
    //    }

    //}
}
