using UnityEngine;
using System.Collections;

public class buildtower3 : MonoBehaviour {

    string buildtower3strng;
    public GameObject buildplace;
    private GameObject go_textrecv;
    private upgtext s_upgtext;
    public GameObject tower;

    void Start()
    {
        buildtower3strng = "Ice Tower\n Attack damage:0.25\n Attack speed:0.75\n Attack range:10\n Cost:150";
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
            s_upgtext.text = buildtower3strng;
    }
}
