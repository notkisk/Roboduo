using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAllLevels : MonoBehaviour
{
    public static UnlockAllLevels instance;
    public int numberOfLevels;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
    }
 


    public void UnlockAll()
    {
        //TODO Unlock all levels
        for (int i = 2; i <= numberOfLevels; i++)
        {
            PlayerPrefs.SetInt("levelAt", i);

        }
    }



}
