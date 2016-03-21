using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    TextMesh tm;
    private int relay;  //annan parentilt edasi calc'ile.
    private int relay2;

	void Start () 
    {
        tm = GetComponent<TextMesh>();
	}
	
	void Update () 
    {
        transform.forward = Camera.main.transform.forward;  //paneb hp kaameraga paralleelseks
	}

    public int current()
    {
        return tm.text.Length;
    }

    public void decrease()
    {
        if (current() > 1)
            tm.text = tm.text.Remove(tm.text.Length - 1); // kui elud pole kriitilised v]tab elu maha
        else
        {
            //teha if, vaatamaks kas l2heb mobil elusid v6i castleil.
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
}