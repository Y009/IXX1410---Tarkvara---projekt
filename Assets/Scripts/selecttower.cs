using UnityEngine;
using System.Collections;

public class selecttower : MonoBehaviour
{

#region initis
    //there's probably a much better way of doing this....

    public GameObject tower;
    private GameObject towercheck;
    private GameObject upgdmgobj;
    private GameObject upgspdobj;
    private GameObject upgrngobj;
    private GameObject sellobj;
    private GameObject cancleobj;
    private clickdmgbtn s_clickdmgbtn;
    private clickspdbtn s_clickspdbtn;
    private clickrngbtn s_clickrngbtn;
    private clicksellbtn s_clicksellbtn;
    private clickcanclebtn s_clickcanclebtn;

    //private bool canvastoggle;
    public GameObject upgcanvas;
#endregion

    void Start () {
        tower = null;
        upgdmgobj = GameObject.Find("upgdmgobj");
        upgspdobj = GameObject.Find("upgspdobj");
        upgrngobj = GameObject.Find("upgrngobj");
        sellobj = GameObject.Find("sellobj");
        cancleobj = GameObject.Find("cancleobj");
        s_clickdmgbtn = upgdmgobj.GetComponent<clickdmgbtn>();
        s_clickspdbtn = upgspdobj.GetComponent<clickspdbtn>();
        s_clickrngbtn = upgrngobj.GetComponent<clickrngbtn>();
        s_clicksellbtn = sellobj.GetComponent<clicksellbtn>();
        s_clickcanclebtn = cancleobj.GetComponent<clickcanclebtn>();
        upgcanvas.GetComponent<Canvas>().enabled = false;
	}
    
    void Update()
    {
        if (tower != towercheck)
        {
            towercheck = tower;
            s_clickdmgbtn.tower = tower;
            s_clickcanclebtn.tower = tower;
            s_clickrngbtn.tower = tower;
            s_clicksellbtn.tower = tower;
            s_clickspdbtn.tower = tower;
        }

    }

    public void toggleupgcanvas()
    {
        upgcanvas.GetComponent<Canvas>().enabled = !upgcanvas.GetComponent<Canvas>().enabled;
    }
}
