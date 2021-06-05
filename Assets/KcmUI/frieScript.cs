using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frieScript : MonoBehaviour
{
   
    public ParticleSystem pv;
    public float firetime;
    public float spawntime;
    public AudioClip clip;

    public bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<ParticleSystem>();

        


    }

    // Update is called once per frame
    void Update()
    {
        spawntime += Time.deltaTime;
       firetime += Time.deltaTime;
        if(firetime > 6.0f)
        {
            pv.Emit(300);

            firetime = 0;          
        }

        if(spawntime > 2.0f)
        {
            first = true;
            spawntime = 0;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if(first == true)
        {
            if (other.tag == "Player")
            {

                Debug.Log("OnParticleCollision");
                Player_Scr.HP = 0;
                SoundManager.instance.SFXPlay("explode", clip);              
                first = false;              
            }
            
        }
        
    }
}
