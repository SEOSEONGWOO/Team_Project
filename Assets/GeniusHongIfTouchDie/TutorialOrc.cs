using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOrc : MonoBehaviour
{
    public Animator avatar;

    public GameObject TutorialBox;

    void Update()
    {
        if (avatar.GetBool("Die") == true)
        {
            TutorialBox.SetActive(true);
        }
    }
}
