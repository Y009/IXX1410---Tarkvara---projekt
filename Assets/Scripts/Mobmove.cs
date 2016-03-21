using UnityEngine;
using System.Collections;

public class Mobmove : MonoBehaviour {
    public int svalue;  //palju skoori annab
    public int mvalue;  //palju raha annab
	// Use this for initialization
	void Start () {
        GameObject castle = GameObject.Find("Castle");
        if (castle)
            GetComponent<NavMeshAgent>().destination = castle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider co)
    {
        // If castle then deal Damage, destroy self
        if (co.name == "Castle")
        {
            co.GetComponentInChildren<Health>().decrease();
            Destroy(gameObject);
        }
    }
}
