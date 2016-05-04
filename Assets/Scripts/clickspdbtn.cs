using UnityEngine;
using System.Collections;

public class clickspdbtn : MonoBehaviour {

    string maxspdstring;
    public GameObject tower;
    private GameObject go_textrecv;
    private upgtext s_upgtext;
    int i = 1;

    void Start()
    {
        go_textrecv = GameObject.Find("upgdesc");
        s_upgtext = go_textrecv.GetComponent<upgtext>();
    }

    public void onClick()
    {
        tower.GetComponent<bullet1>().upgradingtower(i);
    }

    public void sendtext()
    {
        if (tower)
        {
            if (tower.GetComponent<bullet1>().maxspd != tower.GetComponent<bullet1>().maxuplvl)
                maxspdstring = "Upgrade speed by " + tower.GetComponent<bullet1>().spdupg + "\n Current level:" + tower.GetComponent<bullet1>().maxspd + "\n Cost:" + tower.GetComponent<bullet1>().spdcost;
            else
                maxspdstring = "Max level";
            s_upgtext.text = maxspdstring;
        }
    }
}
