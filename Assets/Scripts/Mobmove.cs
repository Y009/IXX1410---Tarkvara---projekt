using UnityEngine;
using System.Collections;

public class Mobmove : MonoBehaviour {
    public int svalue;  //palju skoori annab
    public int mvalue;  //palju raha annab
    public int hp;
    private int hpmax;
    private int relay;

    private attack s_attack;    //v6tan attack scriptist elud
	
	void Start () {
        GameObject castle = GameObject.FindWithTag("castle");
        if (castle)
            GetComponent<NavMeshAgent>().destination = castle.transform.position;
        hpmax = hp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider co)
    {
        if (co.gameObject.tag == "castle")
        {
            //relay = s_attack.dmg;
            co.GetComponentInChildren<Health>().decrease(1);
            Destroy(gameObject);
        }
    }

    public int hpdiff()
    {
        return hpmax - hp;
    }
}








