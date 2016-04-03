using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    TextMesh hptext;
    private int relay;  //annan parentilt edasi calc'ile.
    private int relay2;
    private int hp;
    private int hpmax;

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

    public int current()
    {
        return hp;
    }

    public void decrease(int modhp)
    {
        if (current() > 1)
        {
            hp = hp - modhp; // kui elud pole kriitilised v]tab elu maha
            updatehp();
        }
        else
        {
            //teha if, vaatamaks kas l2heb mobil elusid v6i castleil. / v6i teha castleile oma script...
            Destroy(transform.parent.gameObject);   //kui elud on kriitilised h2vitab uniti
            relay = this.transform.parent.GetComponent<Mobmove>().svalue;
            GameObject go = GameObject.Find("score");
            scorecalc other = (scorecalc)go.GetComponent(typeof(scorecalc));
            other.modifyscore(relay);

            relay2 = this.transform.parent.GetComponent<Mobmove>().mvalue;
            GameObject go2 = GameObject.Find("money");
            moneycalc other2 = (moneycalc)go2.GetComponent(typeof(moneycalc));
            other2.modifymoney(relay2);
        }
    }

    void updatehp()
    {
        hptext.text = hp + "/" + hpmax;
    }    
}