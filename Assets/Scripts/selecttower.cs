using UnityEngine;
using System.Collections;

public class selecttower : MonoBehaviour {

    private float dist = 40;
    public LayerMask towerMask;
    private GameObject tower;

	// Use this for initialization
	void Start () {
        tower = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, dist, towerMask))     //kui raycast saab towerile pihta
            {
                if (tower != null)  
                    tower.GetComponent<bullet1>().upgrading = false;  //kui ei ole esimene tower, siis annan eelmisele teada, et ei upgradei enam
                tower = hit.transform.parent.gameObject;            //panen toweri uueks agmeobjectiks
                tower.GetComponent<bullet1>().upgrading = true;     //anna teada towerile, et kasutab seda
            }
        }
	}
}
