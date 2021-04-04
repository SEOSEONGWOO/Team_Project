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

    // Start is called before the first frame update
    void Start()
    {
        spawnRange = SpawnW.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        delay += Time.deltaTime;
        if (delay < spawninterval)
            return;

        delay = 0;

        Vector3 pos;
        pos.x = Random.Range(spawnRange.x+5, spawnRange.x-5);
        pos.y = Random.Range(-spawnRange.y, spawnRange.y) + 1;
        pos.z = Random.Range(spawnRange.z+5, spawnRange.z-5);

        int type = Random.Range(0, prefab0bject.Length);

        Quaternion rot;
        rot = Quaternion.Euler(Random.Range(0, 0), Random.Range(0, 0), Random.Range(0, 0));

        Instantiate(prefab0bject[type], pos, rot);
    }
}
