using UnityEngine;
using System.Collections;

public class createmaze : MonoBehaviour {

    public GameObject destr;
    int check = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            check = 1;
        else
            check = 0;
    }

    //hoides nr 1'te kustutab klotsid
    void OnMouseEnter()
    {
        if (check == 1)
        {
            Destroy(destr);
        }
    }
}
