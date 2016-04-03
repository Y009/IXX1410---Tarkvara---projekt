using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moneycalc : MonoBehaviour {

    Text moneytxt;
    public static int money;
    void Start()
    {
        moneytxt = GetComponent<Text>();
        money = 100;
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
