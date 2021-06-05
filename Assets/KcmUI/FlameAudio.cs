using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAudio : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

           
     
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            audioSource.Play();

        }
    }
}
