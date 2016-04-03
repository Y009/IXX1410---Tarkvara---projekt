using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bullet1 : MonoBehaviour {

    private float LastShotTime;
    public float AttTime;

    public GameObject bullet;
   // private attack s_attack;
    public List<GameObject> enemiesInRange;

	void Start () {
        enemiesInRange = new List<GameObject>();
        LastShotTime = Time.time;
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
}
