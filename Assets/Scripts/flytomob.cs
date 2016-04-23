using UnityEngine;
using System.Collections;

public class flytomob : MonoBehaviour {

    public Transform target;    //bullet1 annab targeti
    public float dmg;
    public float speed;
    public bool splash;
    public float radius = 4;
    public LayerMask enemylayer;
    private Vector3 splashtarget;
    private float flyTime = 1.0F;
    private Vector3 dir;
    private Rigidbody rigid;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        if (splash == true)
        { 
            splashtarget = target.position;
            dir = splashtarget - transform.position + Vector3.up * 10;
            this.GetComponent<Rigidbody>().velocity = dir.normalized * 30;
        }
    }

	void FixedUpdate () {
        if(target)
            gameObject.transform.rotation = Quaternion.LookRotation(target.position - gameObject.transform.position);

        if (target && !splash)
        {
            Vector3 dir = target.position - transform.position;     //j2llitab vaenlast
            rigid.velocity = dir.normalized * 10;
        }
        else if (splash)
        {                               //mingi normaalne paraboolne trajektoor teha...
            if (transform.position.y > 8)
            {
                dir = dir*0.95f + Vector3.down*7;
                rigid.velocity =  dir;
            }
        }
        else if (!target)
            Destroy(gameObject);

        if (this.transform.position.y < 0)
        {
            splashattack();
        }
	}

    void splashattack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, enemylayer);
        
        foreach (Collider enemy in hitColliders)
        {
            print(enemy);
            enemy.GetComponentInChildren<Health>().decrease(dmg);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider co)
    {
        if(!splash)
        {
            Health health = co.GetComponentInChildren<Health>();    //v6tan mobi elud
            if (health)
            {
                health.decrease(dmg);
                Destroy(gameObject);
            }
        }
    }
}
