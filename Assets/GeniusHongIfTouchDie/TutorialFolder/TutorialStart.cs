using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStart : MonoBehaviour
{
    public GameObject Block;

    public GameObject TutorialZombie;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Block.SetActive(true);
            TutorialZombie.SetActive(true);
            Destroy(gameObject);
        }
    }
}
