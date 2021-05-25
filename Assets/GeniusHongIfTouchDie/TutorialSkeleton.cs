using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkeleton : MonoBehaviour
{
    public Animator avatar;

    public GameObject TutorialOrc;

    void Update()
    {
        if (avatar.GetBool("Die") == true)
        {
            TutorialOrc.SetActive(true);
        }
    }
}
