using UnityEngine;
using System.Collections;

public class clickcanclebtn : MonoBehaviour {

    string canclestring;
    public GameObject tower;
    public GameObject buildplace;
    private GameObject go_textrecv;
    private upgtext s_upgtext;
    int i = 4;

    void Start()
    {
        go_textrecv = GameObject.Find("upgdesc");
        s_upgtext = go_textrecv.GetComponent<upgtext>();
    }

    public void onClick()
    {
        tower.GetComponent<bullet1>().upgradingtower(i);
    }
    public void onClick2()
    {
        buildplace.GetComponent<Buildplace>().selected = false;
    }

    public void sendtext()
    {
        if (tower)
        {
            canclestring = "Close upgrade menu";
            s_upgtext.text = canclestring;
        }
    }
}
