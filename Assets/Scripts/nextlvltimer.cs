using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class nextlvltimer : MonoBehaviour {

    private GameObject go_gui;
    private guiscript s_gui;
    private Text timetxt;
    public float countdowntime;

    private string lvltimestring = "Next Lvl In:";
    private string lvlinfstring = "Next Lvl In: -";

    void Awake()
    {
        go_gui = GameObject.Find("GUI");
        s_gui = go_gui.GetComponent<guiscript>();
        timetxt = GetComponent<Text>();
    }

	void Start () 
    {
        timetxt.text = lvlinfstring;
        settime();
	}

    void FixedUpdate()
    {
        if (!s_gui.startWave && countdowntime > -1 && s_gui.waveEnd && s_gui.check)
        { 
            countdowntime -= Time.deltaTime;
            timetxt.text = lvltimestring + Mathf.Round(countdowntime);
        }
        if (countdowntime <= 0 && s_gui.waveEnd)
        {
            s_gui.startWave = true;
            s_gui.waveEnd = false;
            s_gui.check = true;
            timetxt.text = lvlinfstring;
        }
        if (s_gui.waveActive && countdowntime > 0)
            timetxt.text = lvlinfstring;
    }

    public void settime()
    {
        countdowntime = 11.0f;
        s_gui.waveEnd = false;
    }
}
