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
    }

    void Start()
    {
        waveEnd = false;
        startWave = false;
        check = true;
        s_spawnmobs = go_spawn.GetComponent<spawnmobs>();
        leveltext.CrossFadeAlpha(0.0f, 1, false);
    }

    public void waveinfo(int i, int level, float hp, bool immune, int enemyAmount)
    {
        string lvlstring = null;
        lvlstring = "Current level:";
        lvlstring += level + ". " +enemyAmount;
        if (immune)
            lvlstring += " immune";
        lvlstring += " monsters with " + hp + " health.";
        if (i == 1) 
            lvlstring1 = lvlstring;
        
        leveltext.text = lvlstring1;
        leveltext.CrossFadeAlpha(1.0f, 0, false);
        leveltext.CrossFadeAlpha(0.0f, 10, false);
        //else if (i == 0)
        //{
        //    lvlstring = "Current level:";
        //     else if (i == 0)
        //         lvlstring = "Next level:";
        //    lvlstring += level + ". " + enemyAmount;
        //    if (immune)
        //        lvlstring += " immune";
        //    lvlstring += " monsters with " + hp + " health.";
        //    if (i == 1)
        //        lvlstring2 = lvlstring;
        //    else if (i == 0)
        //    {
        //    print(lvlstring2);
        //    print(i);
        //    print(level);
        //    print(hp);
        //    print(immune);
        //    print(enemyAmount);
        //    print("////////////////////////");
        //       print(lvlstring2);
        //       lvlstring1 = lvlstring;
        //    leveltext.text = lvlstring1;
        //}
    }

    public void toggle()
    {
        startWave = true;
        check = false;
    }
}
