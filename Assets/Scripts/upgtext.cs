using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class upgtext : MonoBehaviour {

    public string text;
    Text upgdesc;

	void Start () {
        upgdesc = GetComponent<Text>();
	}

    public void updatetext()
    {
        upgdesc.text = text;
    }
}
