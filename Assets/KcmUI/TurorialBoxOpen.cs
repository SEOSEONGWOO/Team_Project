using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurorialBoxOpen : MonoBehaviour
{
    public GameObject boxopenQuestion;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            boxopenQuestion.SetActive(true);
            if(Input.GetKeyDown(KeyCode.F))
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            boxopenQuestion.SetActive(false);
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
