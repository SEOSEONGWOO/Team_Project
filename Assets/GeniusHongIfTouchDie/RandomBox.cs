using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public GameObject[] prefab0bject;       // 상자 안에 들어갈 아이템 배열


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);

            int type = Random.Range(0, prefab0bject.Length);

            Instantiate(prefab0bject[type]);
        }
    }
}
