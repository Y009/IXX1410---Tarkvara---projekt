using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class changerectsize : MonoBehaviour {

    public GameObject upgboxrect;
    public GameObject upgboxrect2;

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

    public void changetobig2()
    {
        upgboxrect2.GetComponent<Image>().enabled = true;
    }

    public void changetosmall2()
    {
        upgboxrect2.GetComponent<Image>().enabled = false;
    }
}
