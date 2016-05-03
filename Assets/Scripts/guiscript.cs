
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class guiscript : MonoBehaviour {

    public bool startWave;
    public bool waveEnd;
    public bool check;
    public bool waveActive;
    private GameObject go_spawn;
    private spawnmobs s_spawnmobs;
    private GameObject go_leveltext;
    private Text leveltext;
    private string lvlstring1;  //current level
    private string lvlstring2;  //next level

    void Awake()
    { 
        go_spawn = GameObject.Find("spawn");
        go_leveltext = GameObject.Find("wavetext");
        leveltext = go_leveltext.GetComponent<Text>();
        s_spawnmobs = go_spawn.GetComponent<spawnmobs>();
    }

    void Start()
    {
        waveEnd = false;
        startWave = false;
        check = true;
        leveltext.CrossFadeAlpha(0.0f, 1, false);
    }

    public void waveinfo(int level, float hp, bool immune, int enemyAmount)
    {
        string lvlstring = null;
        lvlstring = "Current level: ";
        lvlstring += level + "\n" +enemyAmount;
        if (immune)
            lvlstring += " immune";
        lvlstring += " monsters with " + hp + " health";
        lvlstring1 = lvlstring;
        
        leveltext.text = lvlstring1;
        StartCoroutine(fadetext());
    }

    public void toggle()
    {
        startWave = true;
        check = false;
    }

    IEnumerator fadetext()
    {
        leveltext.CrossFadeAlpha(1.0f, 0, false);
        yield return new WaitForSeconds(5);
        leveltext.CrossFadeAlpha(0.0f, 2, false);
    }
}