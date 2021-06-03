using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frieScript : MonoBehaviour
{
    public ParticleSystem pv;
    public float firetime;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        firetime += Time.deltaTime;
        if(firetime > 6.0f)
        {
            
            pv.Emit(300);
            firetime = 0;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("OnParticleCollision");
            Player_Scr.HP = 0;
        }
    }
}
