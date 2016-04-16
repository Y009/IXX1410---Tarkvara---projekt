using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Buildplace : MonoBehaviour, IPointerClickHandler
{
    public GameObject towerPrefab;  //tower'i prefabrication
    private GameObject tower;

    private int towerValue;
    private int currentMoney;

    public GameObject money;
    private moneycalc s_moneycalc;

    public LayerMask towerMask;

    void Awake()
    {
        s_moneycalc = money.GetComponent<moneycalc>();
    }

    void Start()
    {
        towerValue -= towerPrefab.GetComponent<bullet1>().towerprice;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buildtower(eventData.pointerPress.transform.position);
    }

    // Kas kursori asukoha juures on tower?
    private bool canPlaceTower(Vector3 checkpos)
    {
        RaycastHit hit;
        if (Physics.Raycast(checkpos, Vector3.up, out hit, 5, towerMask))
        {
            if (hit.collider is MeshCollider)
                return false;
        }
        return true;
    }

    // Kas on piisavalt raha toweri ehitamiseks?
    private bool haveEnoughMoney()
    {

        currentMoney = s_moneycalc.money;
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