using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public Transform SpawnW;
    public GameObject[] prefab0bject; // 좀비배열
    public Vector3 spawnRange;
    public float spawninterval = 5f;
    private float delay = 0f;
    int SpawnInt = 0;
    bool SpawnOn = false;
    public GameObject FinishKey;
    public static int MonsterStack = 0;


    // Start is called before the first frame update
    void Start()
    {
        spawnRange = SpawnW.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(MonsterStack >= 10)
        {
            FinishKey.SetActive(true);
            MonsterStack = 0;
        }

        if(SpawnOn == true)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            if (SpawnInt < 10)
            {
                delay += Time.deltaTime;

                if (delay < spawninterval)
                    return;

                delay = 0;

                Vector3 pos;
                pos.x = Random.Range(spawnRange.x + 5, spawnRange.x - 5);
                pos.y = spawnRange.y;
                pos.z = Random.Range(spawnRange.z + 5, spawnRange.z - 5);

                int type = Random.Range(0, prefab0bject.Length);

                Quaternion rot;
                rot = Quaternion.Euler(Random.Range(0, 0), Random.Range(0, 0), Random.Range(180, 180));

                Instantiate(prefab0bject[type], pos, rot);

                SpawnInt += 1;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SpawnOn = true;
            FinishKey.SetActive(false);
        }
    }
}
