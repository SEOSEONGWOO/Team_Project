using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Block;
    public GameObject TutorialZombie;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TutorialZombie.SetActive(true);
            Block.SetActive(true);
            Destroy(gameObject);
        }
    }
}
