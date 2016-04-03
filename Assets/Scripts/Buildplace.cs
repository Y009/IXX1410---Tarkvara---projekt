using UnityEngine;
using System.Collections;

public class Buildplace : MonoBehaviour
{
    public GameObject towerPrefab;  //tower'i prefabrication
    private GameObject tower;
    public int towerValue;
    private int currentMoney;

    // Kas kursori asukoha juures on tower?
    private bool canPlaceTower()
    {
        return tower == null;
    }

    // Kas on piisavalt raha toweri ehitamiseks?
    private bool haveEnoughMoney()
    {
        currentMoney = moneycalc.money;

        if (currentMoney < towerValue + 200)
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

            GameObject go = GameObject.Find("money");
            moneycalc other = (moneycalc)go.GetComponent(typeof(moneycalc));
            other.modifymoney(towerValue);
        }
    }
}