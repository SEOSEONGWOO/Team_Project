using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartText : MonoBehaviour
{
    public GameObject textPanel;

    public float TextTime = 0.0f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            textPanel.SetActive(true);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextTime += Time.deltaTime;
        if (TextTime > 4)
        {
            textPanel.SetActive(false);
            TextTime = 0;
        }
    }
}
