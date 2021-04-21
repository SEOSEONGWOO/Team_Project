using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    public Animation OpenCheck;
    public Animation CloseCheck;
    public static bool dooronoff;

   
 public void open()
    {
        OpenCheck.Play();
    }
    public void close()
    {
        CloseCheck.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
