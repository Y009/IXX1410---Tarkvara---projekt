using UnityEngine;
using System.Collections;

public class flytomob : MonoBehaviour {

    public Transform target;    //bullet1 annab targeti
    public int dmg;
    public float speed;

	void Start () {
	
	}

	void Update () {
        if (target)
        {
            Vector3 dir = target.position - transform.position;     //j2llitab vaenlast
            this.GetComponent<Rigidbody>().velocity = dir.normalized * 10;
        }
        else
            Destroy(gameObject);


        /*
         * alumine if oli/on selleks kui kuulid ei j2lita vaenlast siis nad ei 
         * eksisteeri nii kaua 
         * kuni mob on elus vaid kuni nad j6uavad maapinnani.
         * Saab kasutada nt spalsh towerite jaoks.
         */
        if (this.transform.position.y < 0)  
            Destroy(gameObject);
	}


    void OnTriggerEnter(Collider co)
    {
        Health health = co.GetComponentInChildren<Health>();    //v6tan mobi elud
        if (health)
        {
            health.decrease(dmg);
            Destroy(gameObject);
        }
    }
}
