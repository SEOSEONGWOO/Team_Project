using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRemove : MonoBehaviour
{
    public GameObject trapRemove;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            trapRemove.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
