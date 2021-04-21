using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public Image img_Skill;

    public bool coolCheck = true;
    public float cooltime = 5.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coolCheck == true)
        { 
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CoolTime(cooltime));
                coolCheck = false;
        }
     }
    }

    IEnumerator CoolTime(float cool)
    {
        print("쿨타임 코루틴 실행");

        while (cool > 0.0f)
        {
            cool -= Time.smoothDeltaTime;

            img_Skill.fillAmount = cool / cooltime;
            yield return new WaitForFixedUpdate();
           
        }

        print("쿨타임 코루틴 완료");
        coolCheck = true;
    }

}
