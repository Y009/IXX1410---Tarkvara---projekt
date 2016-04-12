using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bullet1 : MonoBehaviour {

    public int towerprice = 100;
    public float sellmod = 0.75f; //mitu protsenti annan myyes tagasi
    public int maxuplvl = 5;    //mitu korda luban upgradeida yhte toweri statsi
    private int dpscost = 75, spdcost = 75, rngcost = 75;
    private int maxdps = 0, maxspd = 0, maxrng = 0;
    private int defupadd = 20;

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
        float minHpEnemy =int.MaxValue;                   //initsialiseerib lihtsalt int'ga, aga kuna edasi otsib v2iksemaid siis max int v22rtusega.
        foreach (GameObject enemy in enemiesInRange)    //targetib alati v2himate eludega vastast
        {
            float hpdiff = enemy.GetComponent<Mobmove>().hpdiff();
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
        if (dpscost != 75)      //myyes annan 75% algsest toweri hinnast + viimaste upgradeide raha tagasi.
        {
            sellprice = sellprice - defupadd + dpscost; 
        }
        if (spdcost !=75)
        {
            sellprice = sellprice - defupadd + dpscost;
        }
        if (rngcost != 75)
        {
            sellprice = sellprice - defupadd + rngcost;
        }

        string maxdpsstring = "Upgrade damage\n Current level:" + maxdps + "\n Cost:" + dpscost;
        string maxspdstring = "Upgrade speed\n Current level:" + maxspd + "\n Cost:" + spdcost;
        string maxrngstring = "Upgrade range\n Current level:" + maxrng + "\n Cost:" + rngcost;
        string sellstring = "Sell tower\n Value:" + sellprice;
        string canclestring = "Close upgrade menu";
        Rect dpsrect = new Rect(5, 20, 40, 20);     //kas ma peaks need k6igile andma kasutada ? 
        Rect spdrect = new Rect(50, 20, 40, 20);
        Rect rngrect = new Rect(95, 20, 40, 20);
        Rect sellrect = new Rect(140, 20, 45, 20);
        Rect nvmrect = new Rect(190, 20, 40, 20);
        Rect bigrect = new Rect(20, 20, 235, 100);
        Rect normrect = new Rect(20, 20, 235, 50);

        if (GUI.Button(dpsrect, new GUIContent("DPS", maxdpsstring)))
        {
            print("upg damage +1");
            if (haveEnoughMoney(dpscost) && (maxdps != maxuplvl))
            { 
                dmg += 1;
                s_money.GetComponent<moneycalc>().modifymoney(-dpscost);
                dpscost += defupadd;
                maxdps++;
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }

        else if (GUI.Button(spdrect, new GUIContent("SPD", maxspdstring)))
        {
            print("upg speed");
            if (haveEnoughMoney(spdcost) && (maxspd != maxuplvl))
            {
                AttTime -= 0.1f;
                s_money.GetComponent<moneycalc>().modifymoney(-spdcost);
                spdcost += defupadd;
                maxspd++;
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }

        else if (GUI.Button(rngrect, new GUIContent("RNG", maxrngstring)))
        {
            print("upg range");
            if (haveEnoughMoney(rngcost) && (maxrng != maxuplvl))
            {
                coll.radius += 1;
                s_money.GetComponent<moneycalc>().modifymoney(-rngcost);
                rngcost += defupadd;
                maxrng++;
            }
            else
            {
                Debug.Log("Not enough money.");
            }
        }
        else if (GUI.Button(sellrect, new GUIContent("SELL", sellstring)))
        {
            print("sell tower");
            s_money.GetComponent<moneycalc>().modifymoney(sellprice);
            Destroy(gameObject);
            upgrading = false;
        }
        else if (GUI.Button(nvmrect, new GUIContent("nvm", canclestring)))
        {
            print("cancle upgrade");
            upgrading = false;
        }
        //GUILayout.EndHorizontal();        //dat if line doe
        if (dpsrect.Contains(Event.current.mousePosition) || spdrect.Contains(Event.current.mousePosition) || rngrect.Contains(Event.current.mousePosition) || sellrect.Contains(Event.current.mousePosition) || nvmrect.Contains(Event.current.mousePosition))
            upgRect = bigrect;
        else
            upgRect = normrect;
        GUI.Label(new Rect(20, 45, 150, 70), GUI.tooltip);

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
