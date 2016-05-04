using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

public class bullet1 : MonoBehaviour, IPointerClickHandler
{

#region initis
    public int towerprice = 100;
    public float sellmod = 0.75f;   //mitu protsenti annan myyes tagasi
    public int maxuplvl = 5;        //mitu korda luban upgradeida yhte toweri statsi
    public int dpscost = 75, spdcost = 75, rngcost = 75; 
    public int maxdps = 0, maxspd = 0, maxrng = 0; //curent level
    public int defupadd = 20;       //default upg cost increase

    public float dmgupg;
    public float spdupg = 0.1f;     //kui palju upgrade juurde annab
    public float rngupg = 1f;

    private float LastShotTime;
    public float AttTime;
    public float dmg;
    private SphereCollider coll;
    public bool ice;
    public bool splash;
    public bool slowall = true;     //teha checkbox m2ngijale; kui false siis ice tower targetib ka alati v2ikseimate eludega mobi.

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
        if (Time.timeScale != 0)
        {
            if (s_selecttower.tower != this.gameObject)
                s_selecttower.tower = this.gameObject;    //kui ei ole esimene tower, siis asendab 2ra.
            s_selecttower.upgcanvas.GetComponent<Canvas>().enabled = true;
        }
    }

    void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
        mat = renderer.material;
        s_money = GameObject.Find("money");
        go_GUI = GameObject.Find("GUI");
    }

	void Start () 
    {
        coll = this.gameObject.GetComponent<SphereCollider>();
        target = null;
        s_selecttower = go_GUI.GetComponent<selecttower>();
        UpdateTime = Time.fixedTime + 0.3f;
        upgrading = false;
        enemiesInRange = new List<GameObject>();
        LastShotTime = Time.time;
	}

    void Update()
    {
        updatetarget();
        if (target!=null)                       //kui ei ole sihtm2rki, ei lase
        {
            gameObject.transform.rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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
        else if(ice)
            mat.SetColor("_Color", Color.blue);
        else
            mat.SetColor("_Color", Color.white);
    }

    void updatetarget()         //targetib v2himate eludega mobi, ice toweri puhul mitte juba slowitut, kui slowall == false
    {
        if (enemiesInRange.Count > 0)
        {
            if (ice && slowall)
                enemiesInRange = enemiesInRange.OrderBy(x => x.GetComponent<Mobmove>().slow).ToList();
            else
                enemiesInRange = enemiesInRange.OrderBy(x => x.GetComponent<Mobmove>().hp).ToList();
            target = enemiesInRange[0];
        }
        else
            target = null;
    }

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
        updatetarget();
    }

    void OnTriggerExit(Collider other)          //kui l2heb alast v2lja eemaldab listist
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            destroydelegate del = other.gameObject.GetComponent<destroydelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
        updatetarget();
    }

    public void upgradingtower(int i)
    {   
        switch (i)
        {
            case 0:
                if (haveEnoughMoney(dpscost) && (maxdps != maxuplvl))
                {
                    dmg += dmgupg;
                    s_money.GetComponent<moneycalc>().modifymoney(-dpscost);
                    dpscost += defupadd;
                    maxdps++;
                }
                break;
            
            case 1:
                if (haveEnoughMoney(spdcost) && (maxspd != maxuplvl))
                {
                    AttTime -= spdupg;
                    s_money.GetComponent<moneycalc>().modifymoney(-spdcost);
                    spdcost += defupadd;
                    maxspd++;
                }
                break;

            case 2:
                if (haveEnoughMoney(rngcost) && (maxrng != maxuplvl))
                {
                    coll.radius += rngupg;
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
