using UnityEngine;
using System.Collections;

public class guiscript : MonoBehaviour {

    public bool startWave;
    public bool waveEnd;
    public bool check;
    public bool waveActive;
    private GameObject go_spawn;
    private spawnmobs s_spawnmobs;

    void Start()
    {
        waveEnd = false;
        startWave = false;
        check = true;
        go_spawn = GameObject.Find("spawn");
        s_spawnmobs = go_spawn.GetComponent<spawnmobs>();
    }

    public void toggle()
    {
        startWave = true;
        check = false;
    }
}
