using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillCool2 : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    public TMP_Text textCooldown;
  
    private bool isCooldown = false;
    private float cooldownTime = 5.0f;
    private float cooldownTimer = 0.0f;
    public void start()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }
    public void update()
    {
        if(isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer < 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }
    public void UseSpell()
    {
        if(isCooldown)
        {
            
        }
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cooldownTimer = cooldownTime;
            
        }

    }

   
 

}
