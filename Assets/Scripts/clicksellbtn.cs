using UnityEngine;
using System.Collections;

public class clicksellbtn : MonoBehaviour {

    string sellstring;
    public GameObject tower;
    private GameObject go_textrecv;
    private upgtext s_upgtext;
    int i = 3;

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
            sellstring = "Sell tower\n Value:" + tower.GetComponent<bullet1>().sellprice();
            s_upgtext.text = sellstring;
        }
    }
}
