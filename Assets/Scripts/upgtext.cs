using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class upgtext : MonoBehaviour {

    public string text;
    private Text upgdesc;

	void Start () {
        upgdesc = GetComponent<Text>();
	}

    public void updatetext()
    {
        upgdesc.text = text;
    }
}
