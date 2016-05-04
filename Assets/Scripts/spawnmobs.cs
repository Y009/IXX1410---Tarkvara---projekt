using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //see tuleb 2ra viia siit gui scripti.
using UnityEngine.UI;

public class spawnmobs : MonoBehaviour
{

    #region initis
    private bool waveActive = false;

    public GameObject monsterPrefab;
    public Transform[] spawnPointRoot;

    private int waveLevel = 1;
    private float difficultyMod = 1.0f;
    private float intermissionLength = 10f;
    private int enemyCount = 0;
    private ArrayList enemies;
    private bool allEnemiesSpawned = false;
    private bool gameover = false;
    private float velocity = 5f;
    private float health = 5f;
    private int enemyAmount;
    private int bossAmount = 1;
    private int defenemyAmount = 10;
    private float spawnIntervall = 1f;
    private int waveEndBonus = 25;
    private float bossBonusHP = 272f;

    private GameObject s_money;

    private GameObject castle;
    private GameObject go_timetilnextlvl;
    private nextlvltimer s_nextlvltimer;
    private GameObject go_startwave;
    public GameObject go_GUI;
    private guiscript s_gui;
    private Text btntext;

    private string lvlactivestring = "Level Active";
    private string lvlstartstring = "Start Now";
    private bool immunelevel = false;

	public GUISkin GUI_1;
	float w = (Screen.width/2);
	float h = (Screen.height/2);
    #endregion
	
	
    public enum GameState
    {
        preStart,
        activeWave,
    }

    GameState state = GameState.preStart;

    void Awake()
    {
        s_gui = go_GUI.GetComponent<guiscript>();
        s_money = GameObject.Find("money");
        go_timetilnextlvl = GameObject.Find("timetilnextlvl");
        go_startwave = GameObject.Find("startwave");
        castle = GameObject.FindGameObjectWithTag("castle");
    }

    void Start()
    {
        s_nextlvltimer = go_timetilnextlvl.GetComponent<nextlvltimer>();
        enemies = new ArrayList();
        btntext = go_startwave.GetComponent<Button>().GetComponentInChildren<Text>();
        enemyAmount = defenemyAmount;
    }

    void Update()
    {
        if (castle == null)
        {
            gameover = true;
        }
        switch (state)
        {
            case GameState.preStart:
                if (s_gui.startWave)
                {
                    s_gui.startWave = false;
                    btntext.text = lvlactivestring;
                    go_startwave.GetComponent<Button>().enabled = false ;
                    startNewWave();
                }
                break;

            case GameState.activeWave:
                if (enemyCount == 0 && waveActive && allEnemiesSpawned)
                {
                    finishWave();
                }
                break;
        }

    }

    void LateUpdate()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if ((GameObject)(enemies[i]) == null)
            {
                enemies.Remove(enemies[i]);
            }
        }
        enemyCount = enemies.Count;
    }

    void startNewWave()
    {
        state = GameState.activeWave;
        setNextWave();
        waveLevel++;
        StartCoroutine(StartMission(1.5f));
    }

    IEnumerator StartMission(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        allEnemiesSpawned = false;
        StartCoroutine(EnemySpawnerRoutine(spawnIntervall, enemyAmount));
        waveActive = true;
        s_gui.waveActive = true;
    }

    void setNextWave()
    {
        if (waveLevel % 10 == 0)     //boss lvl
        {
            health += bossBonusHP;
            bossBonusHP += bossBonusHP;
            print(health);
            enemyAmount = 1;
            defenemyAmount += 2;
            bossAmount++;
        }
        else if (waveLevel % 6 == 0)
        {
            health += 7;
            velocity += 0.5f;
        }
        else if (waveLevel % 5 == 0)
        {
            immunelevel = true;
            health += 2;
        }
        else
            health += 5;

        s_gui.waveinfo(waveLevel, health, immunelevel, enemyAmount);
    }

    IEnumerator EnemySpawnerRoutine(float spawnIntervall, int enemyAmount)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            spawnNewEnemy();
            yield return new WaitForSeconds(spawnIntervall);
        }
        allEnemiesSpawned = true;
    }

    void spawnNewEnemy()
    {
        if (castle)
        {
            GameObject mob = (GameObject)Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            Mobmove s_mob = mob.GetComponent<Mobmove>();
            s_mob.hp = health;
            NavMeshAgent nav_mob = mob.GetComponent<NavMeshAgent>();
            nav_mob.speed = velocity;
            s_mob.immune = immunelevel;
            enemyCount++;
            enemies.Add(mob);
        }
    }

    void finishWave()
    {
        if (waveLevel % 11 == 0)
        {
            health -= bossBonusHP;
            enemyAmount = defenemyAmount;
        }
        if (waveLevel == 31)
            StartCoroutine(wingame());
        go_startwave.GetComponent<Button>().enabled = true;
        btntext.text = lvlstartstring;
        s_nextlvltimer.settime();
        waveActive = false;
        s_gui.waveActive = false;
        s_gui.waveEnd = true;
        s_gui.check = true;
        immunelevel = false;
        s_money.GetComponent<moneycalc>().modifymoney(waveEndBonus);
        state = GameState.preStart;
    }

    IEnumerator wingame()
    {
        SceneManager.LoadScene("scene5", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.2f);
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }

    void OnGUI()  //lykata ymber guiscripti ja teha nupu vbajutusele eraldi funkts, yieldiga
    {
		GUI.skin = GUI_1;
		if (gameover)
		{
			GUILayout.BeginArea (new Rect (w-160, h-180, 320, 360));
				GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace ();
					Time.timeScale = 0f;
					GUI.Box(new Rect(0, 0, 320, 360), "Game over");
					if (GUI.Button(new Rect (60, 90, 200, 60), "Restart")){
						Time.timeScale = 1f;
						gameover = false;
                        SceneManager.LoadScene("scene4", LoadSceneMode.Single);
                        System.Threading.Thread.Sleep(250);
                        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
					} else if (GUI.Button(new Rect (60, 180, 200, 60), "Main Menu")){
						SceneManager.LoadScene("scene3", LoadSceneMode.Single);
					    Time.timeScale = 1f;
						System.Threading.Thread.Sleep(250);         //.sleep ei ole v2ga 6ige kasutada, vaja mingi aeg v2lja vahetada
						SceneManager.SetActiveScene(SceneManager.GetActiveScene());
					} else if (GUI.Button(new Rect (60, 270, 200, 60), "Exit")){
						Application.Quit();
					}
					GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
    }
}