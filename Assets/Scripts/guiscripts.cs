using UnityEngine;
using System.Collections;

public class guiscripts : MonoBehaviour
{

    public bool upgrading;
    public Rect upgRect = new Rect(20, 20, 120, 50); //lasta toweril enda pos yle anda

    void Start()
    {
        upgrading = false;
        Debug.Log(upgrading);
    }

    //public void toggle()
    //{
    //    if (upgrading)
    //        upgrading = false;
    //    else
    //        upgrading = true;
    //    Debug.Log(upgrading);
    //}

    void Update()
    {
        if(upgrading)
            Debug.Log(upgrading);
    }

    void OnGUI()
    {
        if (upgrading)
            upgRect = GUI.Window(0, upgRect, towerwindow, "Tower");
    }
    void towerwindow(int ID)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
        {
            print("Got a click");
            upgrading = false;
        }
    }
}