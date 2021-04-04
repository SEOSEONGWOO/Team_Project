using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject BulletEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision other)
    {

        //Instantiate(BulletEffect, transform.position, transform.rotation);

    }
}

   
