using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTraps : MonoBehaviour
{
    public GameObject[] traps;

    public Animation anim1;
    public Animation anim2;
    public Animation anim3;
    public Animation anim4;
    public Animation anim5;
    public Animation anim6;
    public Animation anim7;
    public Animation anim8;
    public Animation anim9;
    public Animation anim10;
    public Animation anim11;
    public Animation anim12;
    public Animation anim13;
    public Animation anim14;
    public Animation anim15;
    public Animation anim16;
    // Start is called before the first frame update
    void Start()
    {
 
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            anim1.Play("Anim_TrapNeedle_Hide");
            anim2.Play("Anim_TrapNeedle_Hide");
            anim3.Play("Anim_TrapNeedle_Hide");
            anim4.Play("Anim_TrapNeedle_Hide");
            anim5.Play("Anim_TrapNeedle_Hide");
            anim6.Play("Anim_TrapNeedle_Hide");
            anim7.Play("Anim_TrapNeedle_Hide");
            anim8.Play("Anim_TrapNeedle_Hide");
            anim9.Play("Anim_TrapNeedle_Hide");
            anim10.Play("Anim_TrapNeedle_Hide");
            anim11.Play("Anim_TrapNeedle_Hide");
            anim12.Play("Anim_TrapNeedle_Hide");
            anim13.Play("Anim_TrapNeedle_Hide");
            anim14.Play("Anim_TrapNeedle_Hide");
            anim15.Play("Anim_TrapNeedle_Hide");
            anim16.Play("Anim_TrapNeedle_Hide");
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim1.Play("Anim_TrapNeedle_Show");
            anim2.Play("Anim_TrapNeedle_Show");
            anim3.Play("Anim_TrapNeedle_Show");
            anim4.Play("Anim_TrapNeedle_Show");
            anim5.Play("Anim_TrapNeedle_Show");
            anim6.Play("Anim_TrapNeedle_Show");
            anim7.Play("Anim_TrapNeedle_Show");
            anim8.Play("Anim_TrapNeedle_Show");
            anim9.Play("Anim_TrapNeedle_Show");
            anim10.Play("Anim_TrapNeedle_Show");
            anim11.Play("Anim_TrapNeedle_Show");
            anim12.Play("Anim_TrapNeedle_Show");
            anim13.Play("Anim_TrapNeedle_Show");
            anim14.Play("Anim_TrapNeedle_Show");
            anim15.Play("Anim_TrapNeedle_Show");
            anim16.Play("Anim_TrapNeedle_Show");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
