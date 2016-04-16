using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class changerectsize : MonoBehaviour {

    public GameObject upgboxrect;

    void start()
    {
        changetosmall();
    }

    public void changetobig()
    {
        upgboxrect.GetComponent<Image>().enabled = true;
    }

    public void changetosmall()
    {
        upgboxrect.GetComponent<Image>().enabled = false;
    }
}
