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

    void Awake()
    {
        s_moneycalc = money.GetComponent<moneycalc>();
    }
    // Kas kursori asukoha juures on tower?
    private bool canPlaceTower()    //kuidas see t66tab?
    {
        return tower == null;
    }

    // Kas on piisavalt raha toweri ehitamiseks?
    private bool haveEnoughMoney()
    {

        currentMoney = s_moneycalc.money;
        Debug.Log(currentMoney);
        if (currentMoney < towerValue + (-2)*(towerValue))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void OnMouseUp()
    {
        if (canPlaceTower() && haveEnoughMoney())
        {
            tower = (GameObject)
            Instantiate(towerPrefab, transform.position + Vector3.up, Quaternion.identity);

            s_moneycalc.modifymoney(towerValue);
        }
    }
}