using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moneycalc : MonoBehaviour {

    Text moneytxt;
    public int money = 500;
    void Start()
    {
        moneytxt = GetComponent<Text>();
        UpdateMoney();
    }

    public void modifymoney(int value)
    {
        money += value;
        UpdateMoney();
    }

    void UpdateMoney()
    {
        moneytxt.text = "Money: " + money;
    }
}
