using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{
    public GameObject[] flametrap;

   
    // Start is called before the first frame update
    void Start()
    {
        flametrap = GameObject.FindGameObjectsWithTag("FlameTrap");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject ft in flametrap)
            {
                ft.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject ft in flametrap)
            {
                ft.SetActive(true);
            }
        }
    }
}
