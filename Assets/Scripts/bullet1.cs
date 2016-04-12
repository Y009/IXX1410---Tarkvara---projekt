using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bullet1 : MonoBehaviour {

    public int towerprice = 100;
    public float sellmod = 0.75f; //mitu protsenti annan myyes tagasi
    public int maxuplvl = 5;    //mitu korda luban upgradeida yhte toweri statsi
    private int dpscost = 75, spdcost = 75, rngcost = 75;
    private int maxdps = 0, maxspd = 0, maxrng = 0;

    private float LastShotTime;
    public float AttTime;
    public float dmg;
    private SphereCollider coll;

    private GameObject s_money;

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
        coll = this.gameObject.GetComponent<SphereCollider>();
        target = null;
        s_money = GameObject.Find("money");
        go_GUI = GameObject.Find("GUI");
        UpdateTime = Time.fixedTime + 30.0f;
        upgrading = false;
        enemiesInRange = new List<GameObject>();
        LastShotTime = Time.time;
        upgRect = new Rect(20, 20, 235, 50); //@inputmouse pos
	}

    void Update()
    {
        target = null;
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
        Pew.GetComponent<flytomob>().dmg = dmg;
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
        int sellprice = (int) (towerprice*sellmod);
        if (dpscost != 75)    //myyes annan 75% algsest toweri hinnast + viimaste upgradeide raha tagasi.
        { 
            sellprice = sellprice - 20 + dpscost; //20 initida default upgrade price'ina
        }
        if (spdcost !=75)
        {
            sellprice = sellprice - 20 + dpscost;
        }
        if (rngcost != 75)
        {
            sellprice = sellprice - 20 + rngcost;
        }
        


        //GUILayout.BeginHorizontal();
        if (GUI.Button(new Rect(5, 20, 40, 20), "DPS"))
        {
            print("upg damage +1");
            if (haveEnoughMoney(dpscost) && (maxdps != maxuplvl))
            { 
                dmg += 1;
                s_money.GetComponent<moneycalc>().modifymoney(-dpscost);
                dpscost += 10;
                //upgrading = false;
                maxdps++;
                print(maxdps);
                print(dpscost);
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }

        else if (GUI.Button(new Rect(50, 20, 40, 20), "SPD"))
        {
            print("upg speed");
            if (haveEnoughMoney(spdcost) && (maxspd != maxuplvl))
            {
                AttTime -= 0.1f;
                s_money.GetComponent<moneycalc>().modifymoney(-spdcost);
                spdcost += 20;
                //upgrading = false;
                maxspd++;
                print(maxspd);
                print(spdcost);
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }

        else if (GUI.Button(new Rect(95, 20, 40, 20), "RNG"))
        {
            print("upg range");
            if (haveEnoughMoney(rngcost) && (maxrng != maxuplvl))
            {
                coll.radius += 1;
                s_money.GetComponent<moneycalc>().modifymoney(-rngcost);
                rngcost += 20;
                maxrng++;
                print(maxrng);
                print(rngcost);
                //upgrading = false;
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }
        else if (GUI.Button(new Rect(140, 20, 45, 20), "SELL"))
        {
            print("sell tower");
            s_money.GetComponent<moneycalc>().modifymoney(sellprice);
            Destroy(gameObject);
            //if (haveEnoughMoney(dpscost))
            //{
            //    dmg += 1;
            //    dpscost += 10;
            //}
            //else
            //{
            //    Debug.Log("Not enough money.");
            //}

            upgrading = false;
        }
        else if (GUI.Button(new Rect(190, 20, 40, 20), "nvm"))
        {
            print("cancle upgrade");
            upgrading = false;
        }
        //GUILayout.EndHorizontal();

    }

    void upgradingtower()
    { 
    //panna k6ik upgradeid siia, v6ttes vatu mida upg ja cost, et oleks modulaarne
    }

    private bool haveEnoughMoney(int cost)
    {

        int currentMoney = s_money.GetComponent<moneycalc>().money;
        if (currentMoney >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
