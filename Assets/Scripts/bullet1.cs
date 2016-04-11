using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bullet1 : MonoBehaviour {

    private float LastShotTime;
    public float AttTime;

    private GameObject go_GUI;
    public GameObject bullet;
    private GameObject target;

    public List<GameObject> enemiesInRange;
    //public LayerMask towerMask;
    //public LayerMask buildMask;
    //private float dist = 40;        //raycastiga v2ljasaadetud kiire distance

    private Renderer renderer;
    private Material mat;

    public bool upgrading;
    public Rect upgRect;

    private float UpdateTime;       //fixed updateis aeglustamiseks veelgi

    void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
        mat = renderer.material;
    }

	void Start () 
    {
        target = null;
        go_GUI = GameObject.Find("GUI");
        UpdateTime = Time.fixedTime + 30.0f;
        upgrading = false;
        enemiesInRange = new List<GameObject>();
        LastShotTime = Time.time;
        upgRect = new Rect(20, 20, 120, 50); //@inputmouse pos
	}

    void Update()
    {
        int minHpEnemy =int.MaxValue;                   //initsialiseerib lihtsalt int'ga, aga kuna edasi otsib v2iksemaid siis max int v22rtusega.
        foreach (GameObject enemy in enemiesInRange)    //targetib alati v2himate eludega vastast
        {
            int hpdiff = enemy.GetComponent<Mobmove>().hpdiff();
            if (hpdiff < minHpEnemy)
            {
                target = enemy;
                minHpEnemy = hpdiff;
            }
        }

        if (target!=null)                       //kui ei ole sihtm2rki, ei lase
        {
            if(Time.time - LastShotTime > AttTime)      //kas on piisavalt aega m66dunud viimasest laskmisest
            {
                Shoot(target.GetComponent<Collider>());
                LastShotTime = Time.time;       //uus viimaselaskmise aeg
            }
        }
    }

    void LateUpdate()
    {
        if (upgrading)
            mat.SetColor("_Color", Color.green);
        else
            mat.SetColor("_Color", Color.white);
    }

    //void FixedUpdate()                // .Sort() on vaja ylevaadata, et null'iga t66taks korralikult / kasutada OrderBy()'d
    //{
    //    if (Time.fixedTime >= UpdateTime)
    //    {
    //        enemiesInRange.Sort();
    //        target = enemiesInRange[0];
    //        UpdateTime = Time.fixedTime + 30.0f;
    //    }
    //}

    void Shoot(Collider co)
    {
        GameObject Pew = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity); //teeb kuuli
        Pew.GetComponent<flytomob>().dmg = this.GetComponentInParent<attack>().dmg;
        Pew.GetComponent<flytomob>().target = co.transform;
    }

    void OnEnemyDestroy(GameObject enemy)       //v6tab listist 2ra kui sureb
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter(Collider other)         //kui j6uab triggeri alasse, lisab listi
    {

        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            destroydelegate del = other.gameObject.GetComponent<destroydelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit(Collider other)          //kui l2heb alast v2lja eemaldab listist
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            destroydelegate del = other.gameObject.GetComponent<destroydelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    void OnGUI()
    { 
        if (upgrading)
            upgRect = GUI.Window(0, upgRect, towerwindow, "Tower");
    }

    void towerwindow(int ID)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
        {
            print("Got a click");
            upgrading = false;
        }
    }
}
