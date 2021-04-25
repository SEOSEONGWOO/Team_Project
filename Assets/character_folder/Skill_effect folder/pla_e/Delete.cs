using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("d");
    }

	// Update is called once per frame
	IEnumerator d()
	{

		yield return new WaitForSeconds(5.0f);
		
		Destroy(gameObject);
		//skill2_Effect.SetActive(false);
		//skill2_Effect.SetActive(false);
	}
     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") //Player 태그가진 캐릭터가 공격 범위 나가면 
        {
            
        }
    }
}
