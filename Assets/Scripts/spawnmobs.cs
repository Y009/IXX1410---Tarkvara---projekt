using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class spawnmobs : MonoBehaviour {
     
     private bool waveActive = false;

     public GameObject monsterPrefab;
     public Transform[] spawnPointRoot;
     
     private int waveLevel = 0;
     private float diffucultyMultiplier = 1.0f;
     private float intermissionLength = 10f;
     private int enemyCount = 0;
     private ArrayList enemies;
     private bool allEnemiesSpawned = false;
     bool gameover = false;
     private float velocity = 4f;
     private float health = 20f;
     private int enemyAmount = 10;
     private float spawnIntervall = 1;
     
    // private GUIScript gui; vaja see teha
     
     public enum GameState {
         preStart,
         activeWave,
         intermission
     }
     
     GameState state = GameState.preStart;
     
     void Start(){
         enemies = new ArrayList();
        // gui = Camera.main.GetComponentInChildren<GUIScript>();
     }
     
     void Update () {
         if (GameObject.Find("Castle") == false)
             gameover = true;
         switch(state){
             
             case GameState.preStart:
                // if(gui.startWave){
                     setNextWave();
                     startNewWave();
                    // gui.startWave = false;
                // }else {
                     
                // }
             break;
             
             case GameState.activeWave:
                 if(enemyCount == 0 && waveActive && allEnemiesSpawned){
                     finishWave();
                 }
             break;
             
             case GameState.intermission:
             break;
         }
     }
     
     void LateUpdate(){
         for(int i = 0; i < enemies.Count; i++){
             if((GameObject)(enemies[i]) == null){
                 enemies.Remove(enemies[i]);
             }
         }
         enemyCount = enemies.Count;
     }
     
     void setNextWave(){
         diffucultyMultiplier = (diffucultyMultiplier * waveLevel) / 2;
     }
     
     void startNewWave(){
         state = GameState.activeWave;
         StartCoroutine(StartMission(1.5f));
         waveLevel++;
     }
     
     IEnumerator InterMission(float seconds){
         yield return new WaitForSeconds(seconds);
         setNextWave();
         startNewWave();
     }
     
     IEnumerator EnemySpawnerRoutine(float spawnIntervall, int enemyAmount, float velocity, float health){
         for(int i = 0; i < enemyAmount; i++){
             spawnNewEnemy(velocity, health);
             yield return new WaitForSeconds(spawnIntervall);
         }
         allEnemiesSpawned = true;
     }
     
     void finishWave(){
         StartCoroutine("InterMission",intermissionLength);
         state = GameState.intermission;
         waveActive = false;
     }
     
     void spawnNewEnemy(float velocity, float health){

         if ((GameObject.Find("Castle(Clone)")) || (GameObject.Find("Castle")))
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);
         // GameObject e = (GameObject) Instantiate(enemy, new Vector3(0,0,0), Quaternion.identity);
         //EnemyScript es = e.GetComponentInChildren<EnemyScript>();
         //int i = Random.Range(0,2);
         //es.setWaypoints(spawnPointRoot[i]);
         //es.maxHealth = health;
         //es.currHealth = health;
         //es.speed = velocity;
         //enemyCount++;
         //enemies.Add(e);
     }
     
     IEnumerator StartMission(float seconds){
         yield return new WaitForSeconds(seconds);
         allEnemiesSpawned = false;
         StartCoroutine(EnemySpawnerRoutine(spawnIntervall,enemyAmount,velocity,health));
         waveActive = true;
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
                 //rego to scene
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

