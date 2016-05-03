using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Buildplace : MonoBehaviour, IPointerClickHandler
{
    private GameObject tower;

    private int towerValue;
    private int currentMoney;

    public GameObject money;
    private moneycalc s_moneycalc;

    public LayerMask towerMask;

    private GameObject go_GUI;
    public selecttower s_selecttower;
    private Vector3 clickloc;

    private Renderer renderer;
    private Material mat;

    public bool selected;

    void Awake()
    {
        s_moneycalc = money.GetComponent<moneycalc>();
        renderer = gameObject.GetComponent<Renderer>();
        go_GUI = GameObject.Find("GUI");
        s_selecttower = go_GUI.GetComponent<selecttower>();
    }

    void Start()
    {
        mat = renderer.material;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            if (s_selecttower.buildplace != this.gameObject)
                s_selecttower.buildplace = this.gameObject;
            s_selecttower.upgcanvas2.GetComponent<Canvas>().enabled = true;
            selected = true;
            clickloc = eventData.pointerPress.transform.position;
        }
    }

    void LateUpdate()
    {
        if (s_selecttower.buildplace == this.gameObject && selected)
            mat.SetColor("_Color", Color.green);
        else
            mat.SetColor("_Color", Color.white);
    }

    // Kas kursori asukoha juures on tower?
    private bool canPlaceTower()
    {
        RaycastHit hit;
        if (Physics.Raycast(clickloc, Vector3.up, out hit, 5, towerMask))
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

    public void buildtower(GameObject tower)
    {
        towerValue -= tower.GetComponent<bullet1>().towerprice;
        if (haveEnoughMoney() && canPlaceTower())
        {
            Instantiate(tower, clickloc + Vector3.up, Quaternion.identity);

            s_moneycalc.modifymoney(towerValue);
        }
    }

}