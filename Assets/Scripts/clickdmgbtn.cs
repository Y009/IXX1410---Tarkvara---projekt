using UnityEngine;
using System.Collections;

public class clickdmgbtn : MonoBehaviour
{

#region initis
    string maxdpsstring;
    public GameObject tower;
    private GameObject go_textrecv;
    private upgtext s_upgtext;
    int i = 0;

#endregion
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
            if (tower.GetComponent<bullet1>().maxdps != tower.GetComponent<bullet1>().maxuplvl)
                maxdpsstring = "Upgrade damage by " + tower.GetComponent<bullet1>().dmgupg + "\n Current level:" + tower.GetComponent<bullet1>().maxdps + "\n Cost:" + tower.GetComponent<bullet1>().dpscost;
            else
                maxdpsstring = "Max level";
            s_upgtext.text = maxdpsstring;
        }
    }

}
