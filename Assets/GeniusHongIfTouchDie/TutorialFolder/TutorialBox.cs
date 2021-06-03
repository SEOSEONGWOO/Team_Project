using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBox : MonoBehaviour
{
    public GameObject[] prefab0bject;       // 상자 안에 들어갈 아이템 배열

    public GameObject Gobj;

    public GameObject Block;

    public Transform Chest;

    public Vector3 ChestVec;

    float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Gobj = GameObject.FindWithTag("Player");

        ChestVec = Chest.transform.position;

        distance = Vector3.Distance(ChestVec, Player_Scr.CLC);

        if (distance < 5.0f)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                int type = Random.Range(0, prefab0bject.Length);
                Debug.Log(type);
                Instantiate(prefab0bject[type], ChestVec + this.transform.up * 1.5f, Quaternion.identity);
                Destroy(Block);
                Destroy(gameObject);
            }
        }
    }
}
