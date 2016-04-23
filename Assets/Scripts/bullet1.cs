using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class bullet1 : MonoBehaviour, IPointerClickHandler
{

#region initis
    public int towerprice = 100;
    public float sellmod = 0.75f; //mitu protsenti annan myyes tagasi
    public int maxuplvl = 5;    //mitu korda luban upgradeida yhte toweri statsi
    public int dpscost = 75, spdcost = 75, rngcost = 75;
    public int maxdps = 0, maxspd = 0, maxrng = 0;
    public int defupadd = 20;

    private bool splash = false;
    private float LastShotTime;
    public float AttTime;
    public float dmg;
    private SphereCollider coll;

    private GameObject s_money;

    private GameObject go_GUI;
    public selecttower s_selecttower; 
    public GameObject bullet;
    private GameObject target;

    public List<GameObject> enemiesInRange;

    private Renderer renderer;
    private Material mat;

    public bool upgrading;
    public Rect upgRect;

    private float UpdateTime;       //fixed updateis aeglustamiseks veelgi

#endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (s_selecttower.tower != this.gameObject)
            s_selecttower.tower = this.gameObject;    //kui ei ole esimene tower, siis asendab 2ra.
        s_selecttower.upgcanvas.GetComponent<Canvas>().enabled = true;
    }

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
        s_selecttower = go_GUI.GetComponent<selecttower>();
        UpdateTime = Time.fixedTime + 0.3f;
        upgrading = false;
        enemiesInRange = new List<GameObject>();
        LastShotTime = Time.time;
        if (gameObject.tag == "splashtwr")
            splash = true;
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
            gameObject.transform.rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position);
            if(Time.time - LastShotTime > AttTime)      //kas on piisavalt aega m66dunud viimasest laskmisest
            {
                Shoot(target.GetComponent<Collider>());
                LastShotTime = Time.time;       //uus viimaselaskmise aeg
            }
        }
    }

    void LateUpdate()
    {
        if (s_selecttower.tower == this.gameObject &&  s_selecttower.upgcanvas.GetComponent<Canvas>().enabled)
            mat.SetColor("_Color", Color.green);
        else
            mat.SetColor("_Color", Color.white);
    }

    //void FixedUpdate()                // .Sort() on vaja ylevaadata, et null'iga t66taks korralikult / kasutada OrderBy()'d
    //{
    //    if (!(enemiesInRange.count = 0))
    //    {
    //        if (Time.fixedTime >= UpdateTime)
    //        {
    //            enemiesInRange.Sort();
    //            target = enemiesInRange[0];
    //            UpdateTime = Time.fixedTime + 0.3f;
    //        }
    //    }
    //}

    void Shoot(Collider co)
    {
        GameObject Pew = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity); //teeb kuuli
        Pew.GetComponent<flytomob>().dmg = dmg;
        Pew.GetComponent<flytomob>().target = co.transform;
        //if (splash)
        //    Pew.GetComponent<flytomob>().splash = true;
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

    public void upgradingtower(int i)
    {   
        switch (i)
        {
            case 0:
                if (haveEnoughMoney(dpscost) && (maxdps != maxuplvl))
                {
                    dmg += 1;
                    s_money.GetComponent<moneycalc>().modifymoney(-dpscost);
                    dpscost += defupadd;
                    maxdps++;
                }
                break;
            
            case 1:
                if (haveEnoughMoney(spdcost) && (maxspd != maxuplvl))
                {
                    AttTime -= 0.1f;
                    s_money.GetComponent<moneycalc>().modifymoney(-spdcost);
                    spdcost += defupadd;
                    maxspd++;
                }
                break;

            case 2:
                if (haveEnoughMoney(rngcost) && (maxrng != maxuplvl))
                {
                    coll.radius += 1;
                    s_money.GetComponent<moneycalc>().modifymoney(-rngcost);
                    rngcost += defupadd;
                    maxrng++;
                }
                break;

            case 3:
                s_money.GetComponent<moneycalc>().modifymoney(sellprice());
                Destroy(gameObject);
                s_selecttower.toggleupgcanvas();
                upgrading = false;
                break;
            case 4:
                s_selecttower.toggleupgcanvas();
                upgrading = false;
                break;

        }
    }

    public int sellprice()
    {
        int sellprice = (int)(towerprice * sellmod);
        if (dpscost != 75)      //myyes annan 75% algsest toweri hinnast + viimaste upgradeide raha tagasi.
            sellprice = sellprice - defupadd + dpscost;
        if (spdcost != 75)
            sellprice = sellprice - defupadd + dpscost;
        if (rngcost != 75)
            sellprice = sellprice - defupadd + rngcost;
        return sellprice;
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
