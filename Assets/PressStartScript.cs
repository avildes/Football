using UnityEngine;
using System.Collections;

public class PressStartScript : MonoBehaviour {

	void Start ()
    {
        StartCoroutine(Flash());
	}
	
	void Update ()
    {
	    if(Input.touchCount == 1 || Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("game");
        }
	}

    IEnumerator Flash()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(.5f);
        

        StartCoroutine(Flash());
    }
}
