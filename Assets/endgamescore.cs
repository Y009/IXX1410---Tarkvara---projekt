using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class endgamescore : MonoBehaviour {

    private GameObject stats;
    private globalscript s_globalscript;
    public string text;
    Text upgdesc;

    void Start()
    {
        stats = GameObject.FindWithTag("globalobj");
        s_globalscript = stats.GetComponent<globalscript>();
        upgdesc = GetComponent<Text>();
        text = "Score : " + s_globalscript.skoor;
        updatetext();

    }

    public void updatetext()
    {
        upgdesc.text = text;
    }
}
