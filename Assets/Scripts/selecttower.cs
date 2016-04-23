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

    private GameObject buildtowerobj1;
    private GameObject buildtowerobj2;
    private GameObject buildtowerobj3;
    private GameObject buildtowerobj4;
    private buildtower1 s_buildtower1;
    private buildtower2 s_buildtower2;    //lisa towerite jaoks
    //private buildtower3 s_buildtower3;
    //private buildtower4 s_buildtower4;
    //private buildcancle s_buildcancle;

    public GameObject upgcanvas;
    public GameObject upgcanvas2;

    public GameObject buildplace;
    private GameObject buildplacecheck;

#endregion

    void Start () {
        tower = null;
        buildplace = null;
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

        upgcanvas2.GetComponent<Canvas>().enabled = false;
        buildtowerobj1 = GameObject.Find("buildtowerobj");
        buildtowerobj2 = GameObject.Find("buildtower2obj");
        buildtowerobj3 = GameObject.Find("buildtower3obj");
        buildtowerobj4 = GameObject.Find("buildtower4obj");
        s_buildtower1 = buildtowerobj1.GetComponent<buildtower1>();
        s_buildtower2 = buildtowerobj2.GetComponent<buildtower2>();
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
        if (buildplace != buildplacecheck)
        {
            buildplacecheck = buildplace;
            s_buildtower1.buildplace = buildplace;
            s_clickcanclebtn.buildplace = buildplace;
            s_buildtower2.buildplace = buildplace;    
            //s_buildtower3.buildplace = buildplace;        //need on lisa towerite jaoks
            //s_buildtower4.buildplace = buildplace;
        }


    }

    public void toggleupgcanvas()
    {
        upgcanvas.GetComponent<Canvas>().enabled = !upgcanvas.GetComponent<Canvas>().enabled;
    }

    public void toggleupgcanvas2()
    {
        upgcanvas2.GetComponent<Canvas>().enabled = !upgcanvas2.GetComponent<Canvas>().enabled;
    }
}
