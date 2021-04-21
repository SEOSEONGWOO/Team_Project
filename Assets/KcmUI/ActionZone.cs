using System.Collections;
using UnityEngine;

public class ActionZone : MonoBehaviour
{
    public GameObject Target;

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
           
        }
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
