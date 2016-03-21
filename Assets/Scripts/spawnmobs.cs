using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class spawnmobs : MonoBehaviour {

    public GameObject monsterPrefab;
    public GameObject[] plebs;      //mob'id kes on p2rast gg'd alles on plebid
//    private NavMeshAgent agent;     //et saaks kontrollida mobi kyljes olevaid navmeshagent'i v22rtusi


    public float interval = 1;
    bool gameover = false;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnNext", interval, interval);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Castle")==false)
            gameover = true;
	}

    void SpawnNext()
    {
        if ((GameObject.Find("Castle(Clone)")) || (GameObject.Find("Castle")))
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        else
        {
            Destroy(transform.gameObject);
            plebs = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject pleb in plebs)
                Destroy(pleb);
        }
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
            else if (GUILayout.Button("EXIT"));
                Application.Quit();

       }
    }
}
