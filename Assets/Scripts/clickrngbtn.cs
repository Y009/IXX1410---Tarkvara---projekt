﻿using UnityEngine;
using System.Collections;

public class clickrngbtn : MonoBehaviour {

    string maxrngstring;
    public GameObject tower;
    private GameObject go_textrecv;
    private upgtext s_upgtext;
    int i = 2;

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
            maxrngstring = "Upgrade range\n Current level:" + tower.GetComponent<bullet1>().maxrng + "\n Cost:" + tower.GetComponent<bullet1>().rngcost;
            s_upgtext.text = maxrngstring;
        }
    }
}
