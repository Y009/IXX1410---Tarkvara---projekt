using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bullet1 : MonoBehaviour {

    private float LastShotTime;
    public float AttTime;

    //public GameObject GUI;
    //private guiscript s_gui;
    public GameObject bullet;

    public List<GameObject> enemiesInRange;
    public LayerMask towerMask;
    public LayerMask buildMask;
    private float dist = 40;        //raycastiga v2ljasaadetud kiire distance

    private Renderer renderer;
    private Material mat;
    //private Color color;
    //private Color defaultcolor;
    private bool upgrading;
    public Rect upgRect;
   // private int emission = 656565; //custom emission kui tower on selectitud. default on 0

    void Awake()
    {
       // s_gui = GUI.GetComponent<guiscript>();
        renderer = gameObject.GetComponent<Renderer>();
        mat = renderer.material;
    }

	void Start () 
    {
        //defaultcolor = Color.white * 0;
        //color = Color.white * emission;
        upgrading = false;
        enemiesInRange = new List<GameObject>();
        LastShotTime = Time.time;
        upgRect = new Rect(20, 20, 120, 50); //@inputmouse pos
	}

    void Update()
    {
        GameObject target = null;
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
            
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
           // Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(Camera.main.transform.position, (transform.position - Camera.main.transform.position) * 10, Color.green, 5);
            if (Physics.Raycast(ray, out hit, dist, buildMask))
            {
                Debug.Log(hit.collider);            // miks ma k6ik saan?




                //float y = 0;
                //float x = hit.transform.position.x;
                //float z = hit.transform.position.z;
                //Vector3 pos = new Vector3(x, y, z);

                if (Physics.Raycast(hit.point, Vector3.up, out hit, 5, towerMask))
                {
                    if (hit.collider is CapsuleCollider)
                    {
                        //s_gui.Tower = this.gameObject;
                        //s_gui.toggle();  
                        upgrading = true;
                        Debug.Log(hit.collider);
                        //Debug.Log(hit.transform.position);
                    }                
                }
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

    //void OnMouseOver()
    //{
    //    //Debug.Log("lyfe sux");
    //    // Destroy(gameObject);
    //    //s_gui.upgrading = true;
    //    //Debug.Log(s_gui.upgrading + "bullet1");
    //    s_gui.toggle();
    //}

    //if (Input.GetMouseButtonDown(0))
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        if (hit.transform.tag == "tower")
    //        { 
    //            s_gui.upgrading = true;
    //            Debug.Log("werk");
    //        }
    //    }

    //}
}
