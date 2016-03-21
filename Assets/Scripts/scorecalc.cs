using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scorecalc : MonoBehaviour {

    Text skoortxt;
    private int score;
    void Start()
    {
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
    }
}
