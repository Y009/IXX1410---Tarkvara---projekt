using UnityEngine;
using System.Collections;

public class Buildplace : MonoBehaviour
{
    public GameObject towerPrefab;  //tower'i prefabrication
    private GameObject tower;

    public int towerValue;
    private int currentMoney;

    public GameObject money;
    private moneycalc s_moneycalc;

    //raycasti jaoks
    private float maxdist = 60;     
    public LayerMask builplaceMask; //maskid on lihtsalt 2^9 ja 2^8 (^ - astmes)
    public LayerMask towerMask;

    void Awake()
    {
        s_moneycalc = money.GetComponent<moneycalc>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //kontrollin kas klikitakse buildplacei peale, s.o asendus OnMouseUp'le kuna too tekitas konflikte  
        if (Input.GetMouseButtonDown(0))
        {   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxdist, builplaceMask))
            {
                buildtower(hit.transform.position);
            }
        }
    }

    // Kas kursori asukoha juures on tower?
    private bool canPlaceTower(Vector3 checkpos)
    {
        RaycastHit hit;
        if (Physics.Raycast(checkpos, Vector3.up, out hit, 5, towerMask))
        {
            if (hit.collider is CapsuleCollider)
                return false;
        }
        return true;
    }

    // Kas on piisavalt raha toweri ehitamiseks?
    private bool haveEnoughMoney()
    {

        currentMoney = s_moneycalc.money;
       // Debug.Log(currentMoney);
        if (currentMoney < towerValue + (-2)*(towerValue))      //kuna toweri v22rtus on negatiivne
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void buildtower(Vector3 buildplaceLoc)
    {
        if (haveEnoughMoney() && canPlaceTower(buildplaceLoc))
        {
            tower = (GameObject)
            Instantiate(towerPrefab, buildplaceLoc + Vector3.up, Quaternion.identity);

            s_moneycalc.modifymoney(towerValue);
        }
    }
}