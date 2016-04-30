using UnityEngine;
using System.Collections;

public class Mobmove : MonoBehaviour {

    public int svalue;  //palju skoori annab
    public int mvalue;  //palju raha annab
    public float hp;
    private float hpmax;
    public bool iceslow = false;
    private bool slow = false;

    private float hittime;

    private Renderer renderer;
    private Material mat;
    private NavMeshAgent nav_mob;

    private int normspeed;
    public int slowspeed = 2;
    public float slowedtime = 1.5f;
    public bool immune;

    void Awake()
    {
        nav_mob = GetComponent<NavMeshAgent>();
        renderer = gameObject.GetComponent<Renderer>();
        mat = renderer.material;
        normspeed = (int) nav_mob.speed;
    }

	void Start () {
        GameObject castle = GameObject.FindWithTag("castle");
        if (castle)
            GetComponent<NavMeshAgent>().destination = castle.transform.position;
        hpmax = hp;
	}

    void Update()
    {
        if (iceslow && !immune)
            slowed1();
    }

    void FixedUpdate()
    {
        if (slow && hittime + slowedtime < Time.time)
        { 
            mat.SetColor("_Color", Color.white);
            nav_mob.speed = normspeed;
            slow = false;
        }
    }

    void slowed1()
    {
        iceslow = false;
        slow = true;
        hittime = Time.time;
        nav_mob.speed = slowspeed;
        mat.SetColor("_Color", Color.blue);
    }

    void OnTriggerEnter(Collider co)
    {
        if (co.gameObject.tag == "castle")
        {
            co.GetComponentInChildren<Health>().decrease(1);
            Destroy(gameObject);
        }
    }

    public float hpdiff()
    {
        return hpmax - hp;
    }
}