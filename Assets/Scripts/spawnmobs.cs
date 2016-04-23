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
    private int enemyAmount = 10;
    private float spawnIntervall = 1;
    private int waveEndBonus = 25;

    private GameObject s_money;

    private GameObject castle;
    private GameObject go_timetilnextlvl;
    private nextlvltimer s_nextlvltimer;
    private GameObject go_startlvlbtn;
    public GameObject go_GUI;
    private guiscript s_gui;
    private Text btntext;

    private string lvlactivestring = "Level Active";
    private string lvlstartstring = "Start Now";
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
    }

    void Start()
    {
        s_money = GameObject.Find("money");
        go_timetilnextlvl = GameObject.Find("timetilnextlvl");
        s_nextlvltimer = go_timetilnextlvl.GetComponent<nextlvltimer>();
        go_startlvlbtn = GameObject.Find("startlvlbtn");
        enemies = new ArrayList();
        castle = GameObject.FindGameObjectWithTag("castle");
        btntext = go_startlvlbtn.GetComponent<Button>().GetComponentInChildren<Text>();
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
                    go_startlvlbtn.GetComponent<Button>().enabled = false ;
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
        StartCoroutine(StartMission(1.5f));
        waveLevel++;
        setNextWave();
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
        if (waveLevel % 5 == 0)
        {
            health += 7;
            velocity++;
        }
        else
            health += 5;

        print("made it to setnextwave");
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
            // s_mob.hp = (health * difficultyMod);
            NavMeshAgent nav_mob = mob.GetComponent<NavMeshAgent>();
            nav_mob.speed = velocity;
            enemyCount++;
            enemies.Add(mob);
        }
    }

    void finishWave()
    {
        print("finished wave");
        go_startlvlbtn.GetComponent<Button>().enabled = true; ;
        btntext.text = lvlstartstring;
        s_nextlvltimer.settime();
        waveActive = false;
        s_gui.waveActive = false;
        s_gui.waveEnd = true; //kus ma settime'i callin
        s_gui.check = true;
        s_money.GetComponent<moneycalc>().modifymoney(waveEndBonus);
        state = GameState.preStart;
    }

    void OnGUI()
    {
        if (gameover)
        {
            Time.timeScale = 0f;
            GUILayout.Label("GG");
            if (GUILayout.Button("Restart"))
            {
                Time.timeScale = 1f;
                gameover = false;
            }
            else if (GUILayout.Button("Main Menu"))
            {
                SceneManager.LoadScene("scene3", LoadSceneMode.Single);
                Time.timeScale = 1f;
                System.Threading.Thread.Sleep(250);
                SceneManager.SetActiveScene(SceneManager.GetActiveScene());
            }
            else if (GUILayout.Button("EXIT"))
                Application.Quit();

        }
    }
}