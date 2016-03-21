using UnityEngine;
using System.Collections;

public class attack : MonoBehaviour {

    public GameObject bullet;

    //public float rotationSpeed = 35;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider co)
    {
        if (co.GetComponent<Mobmove>()) //kas mobmove script on kyljes?
        {
            //Vector3 dir = target.position - transform.position;
            //float step = rotationSpeed* Time.deltaTime;
            //Vector3 newdir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            //Debug.DrawRay(transform.position, newdir, Color.red);
            //transform.rotation = Quaternion.LookRotation(newdir);
            GameObject Pew = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            Pew.GetComponent<bullet1>().target = co.transform;
        }
    }
}
