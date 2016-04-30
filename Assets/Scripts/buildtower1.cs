using UnityEngine;
using System.Collections;

public class buildtower1 : MonoBehaviour {

    string buildtower1strng;
    public GameObject buildplace;
    private GameObject go_textrecv;
    private upgtext s_upgtext;
    public GameObject tower;

	void Start () {
        buildtower1strng = "Basic Tower\n Attack damage: 1\n Attack speed: 0.75\n Attack range: 10\n Cost: 100";
        go_textrecv = GameObject.Find("upgdesc2");
        s_upgtext = go_textrecv.GetComponent<upgtext>();
	}
	
    public void onClick()
    {
        buildplace.GetComponent<Buildplace>().buildtower(tower);
    }

    public void sendtext()
    {
        if (buildplace)
            s_upgtext.text = buildtower1strng;
    }
}
