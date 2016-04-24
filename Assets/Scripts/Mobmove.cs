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

    void Awake()
    {
        nav_mob = GetComponent<NavMeshAgent>();
        renderer = gameObject.GetComponent<Renderer>();
        mat = renderer.material;
    }

	void Start () {
        GameObject castle = GameObject.FindWithTag("castle");
        if (castle)
            GetComponent<NavMeshAgent>().destination = castle.transform.position;
        hpmax = hp;
	}

    void Update()
    {
        if (iceslow)
        {
            hittime = Time.time;
            iceslow = false;
            StartCoroutine("slowed1");
        }
    }

    IEnumerator slowed1()
    {
        nav_mob.speed = 2;
        mat.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(1.5f);
        if (hittime + 1.5f < Time.time)
        { 
            mat.SetColor("_Color", Color.white);
            nav_mob.speed = 5;
        }
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