using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FolloFollowme : MonoBehaviour
{
    public Animator avatar;

    public Transform firstlo;
    private NavMeshAgent nav;

    public GameObject BRCM;

    public GameObject ZBC;

    public Vector3 fl1, fl2, fl3;

    public int test555;
    int Chp = 100;

    float delay = 0f;


    private void Start()
    {
        avatar = GetComponent<Animator>();
        test555 = 1;
    }

    void Update()
    {
        fl3 = firstlo.transform.position;
        Quaternion rot;
        rot = Quaternion.Euler(Random.Range(0, 0), Random.Range(0, 0), Random.Range(0, 0));

        if (Input.GetButtonDown("Fire1"))
        {
            Chp -= 10;
        }

        if(avatar.GetBool("Die")==true)
        {
            delay += Time.deltaTime;


            if (delay >= 3)
            {
                Destroy(gameObject);
                delay = 0;
            }
        }

        if (Chp <= 0 && avatar.GetBool("Die") == false)
        {
            avatar.SetTrigger("CDie");
            avatar.SetBool("Die", true);

            Instantiate(BRCM, fl3, rot);

        }

        if (avatar.GetBool("Die") == false)
        {
            if (test555 == 1)
            {
                fl1 = firstlo.transform.position;
                fl2 = firstlo.transform.eulerAngles;
                test555 = 0;
            }
            Quaternion WcL = Quaternion.Euler(fl2.x, fl2.y, fl2.z);

            nav = GetComponent<NavMeshAgent>();

            if (avatar.GetBool("Att") == true)
            {
                nav.speed = 0;

            }
            else if(avatar.GetBool("Att") == false)
            {
                if (InsideC.inside == true)
                {
                    nav.speed = 4;
                    nav.SetDestination(Gunner.CLC);
                    avatar.SetBool("LookC", true);
                }
                if (InsideC.inside == false)
                {
                    avatar.SetBool("LookC", false);
                    nav.SetDestination(fl1);
                    if (firstlo.position == fl1)
                    {
                        avatar.SetBool("BackP", false);
                        // firstlo.transform.rotation = Quaternion.Euler(fl2);
                        if (firstlo.transform.eulerAngles != new Vector3(fl2.x, fl2.y, fl2.z))
                        {
                            //firstlo.transform.eulerAngles += new Vector3(0f, 0.1f, 0f);
                            firstlo.transform.rotation = Quaternion.Lerp(firstlo.transform.rotation, WcL, Time.deltaTime * 1);

                        }

                    }
                    else if (firstlo.position != fl1)
                    {
                        nav.speed = 1;
                        avatar.SetBool("BackP", true);
                    }
                }
            }       
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            avatar.SetBool("Att", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            avatar.SetBool("Att", false);
        }
    }
}
