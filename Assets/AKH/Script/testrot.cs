using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testrot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, -10f, 0);
    }
}
