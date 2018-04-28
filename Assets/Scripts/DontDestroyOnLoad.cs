using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1) Destroy(this.gameObject);  //This is so that going back to the menu it won't create another music object

        DontDestroyOnLoad(this.gameObject);
    }
}
