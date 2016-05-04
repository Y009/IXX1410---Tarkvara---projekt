using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scorecalc : MonoBehaviour {

    Text skoortxt;
    private int score;
    public GameObject go_globalobj;
    private globalscript s_globalscript;

    void Awake()
    {
        go_globalobj = GameObject.Find("globalobj");    
    }

    void Start()
    {
        s_globalscript = go_globalobj.GetComponent<globalscript>();
        skoortxt = GetComponent<Text>();
        score = 0;
        UpdateScore();
	}

    public void modifyscore(int value)
    {
        score +=value;
        UpdateScore();
    }

    void UpdateScore()
    {
        skoortxt.text = "Score: " + score;
        s_globalscript.skoor = score;
    }
}
