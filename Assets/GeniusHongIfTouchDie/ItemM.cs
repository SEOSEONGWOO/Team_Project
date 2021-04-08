using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemM : MonoBehaviour
{
    public float rotationSpeed = 100.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("아이템 휙득");
            Destroy(gameObject);
        }
    }
}
