using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHmove : MonoBehaviour
{
    
    public GameObject m_projectileContainer;
    public void OnEventFx(GameObject InEffect)
    {
        Vector3 pos = m_projectileContainer.gameObject.transform.position;//캐릭터 오브젝트의 위치값 가져오기
        Debug.Log(pos);
        //GameObject newSpell = Instantiate(InEffect) as GameObject;
        GameObject newSpell = Instantiate(InEffect, pos + new Vector3(0, 0, 0), transform.rotation) as GameObject;
        //pos의 위치값가져와서 이펙트 생성하기
        newSpell.transform.parent = m_projectileContainer.transform; //캐릭터 오브젝트의 자식으로 이펙트 생성
        Destroy(newSpell, 1.0f);
    }
}
