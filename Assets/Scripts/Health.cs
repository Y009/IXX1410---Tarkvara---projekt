using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    TextMesh hptext;
    private int relay;  //annan parentilt edasi calc'ile.
    private int relay2;
    private float hp;
    private float hpmax;
    private GameObject go;
    private GameObject go2;
    private scorecalc other;
    private moneycalc other2;
    void Awake()
    { 
        go = GameObject.Find("score");
        other = (scorecalc)go.GetComponent(typeof(scorecalc));
        go2 = GameObject.Find("money");
        other2 = (moneycalc)go2.GetComponent(typeof(moneycalc));
    }

	void Start () 
    {
        hptext = GetComponent<TextMesh>();
        if (transform.parent.tag == "Enemy")
            hp = this.GetComponentInParent<Mobmove>().hp;
        else if(transform.parent.tag == "castle")
            hp = this.GetComponentInParent<destroycastle>().hp;
        hpmax = hp;
        updatehp();
	}
	
	void Update () 
    {
        transform.forward = Camera.main.transform.forward;  //paneb hp kaameraga paralleelseks
	}

    void FixedUpdate()
    {
        if (hp <= 0 && transform.parent.tag == "Enemy")
        {
            Destroy(this.transform.parent.gameObject);
            decreaseElse();
            
        }
    }

    public void decrease(float modhp)
    {
        if (hp > 1)
        {
            hp = hp - modhp; // kui elud pole kriitilised v]tab elu maha
            updatehp();
            if (transform.parent.tag == "castle")
                decreasescore();
        }
        else
        {
            decreaseElse();
        }
    }

    void updatehp()
    {
        hptext.text = hp + "/" + hpmax;
    }
    void decreaseElse()
    { 
        decreasescore();
        relay2 = this.transform.parent.GetComponent<Mobmove>().mvalue;
        other2.modifymoney(relay2);
        Destroy(transform.parent.gameObject);   //kui elud on kriitilised h2vitab uniti
    
    }
    void decreasescore()
    {
        if (transform.parent.tag == "castle")
            relay = -50;                        //teha modulaarseks
        else
            relay = this.transform.parent.GetComponent<Mobmove>().svalue;
        other.modifyscore(relay);
    }
}