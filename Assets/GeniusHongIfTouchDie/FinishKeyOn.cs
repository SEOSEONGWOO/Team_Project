using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishKeyOn : MonoBehaviour
{
    public float rotaionSpeed = 100.0f;
    public GameObject Potal;
   // public GameObject Box;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotaionSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("열쇠 획득");
            Destroy(gameObject);
            Potal.SetActive(true);
            //Box.SetActive(true);
        }
    }
}
