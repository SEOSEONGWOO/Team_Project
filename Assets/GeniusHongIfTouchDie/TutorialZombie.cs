using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZombie : MonoBehaviour
{
    public Animator avatar;

    public GameObject TutorialSkeleton;

    void Update()
    {
        if (avatar.GetBool("Die") == true)
        {
            TutorialSkeleton.SetActive(true);
        }
    }
}
