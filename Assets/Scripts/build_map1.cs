using UnityEngine;
using System.Collections;

public class build_map1 : MonoBehaviour
{

    private int check = 0;

    public GameObject uusklots;
    public GameObject castle;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
            check = 1;
        else if (Input.GetKey(KeyCode.Alpha3))
            check = 2;
        else
            check = 0;
        if (Input.GetMouseButton(0)) //hoides nr2'te lisab uue klotsi
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 isempty = new Vector3(0, 0, 0);
            if (Physics.Raycast(ray, out hit))
            {
                float x = (float)Mathf.Round(hit.point.x);
                float y = (float)Mathf.Floor(hit.point.y);
                float z = (float)Mathf.Round(hit.point.z);
                isempty = new Vector3(x, y + 1, z);
            }


            if ((check == 1) && checkIfPosEmpty(isempty))
            {
                GameObject Buildplace = (GameObject)Instantiate(uusklots);
                Buildplace.transform.position = isempty + new Vector3(0, -0.5F, 0);
            }
            if ((check == 2) && checkIfPosEmpty(isempty) && GameObject.FindWithTag("castle") == null)
            {
                GameObject Castle = (GameObject)Instantiate(castle);
                Castle.transform.position = isempty + new Vector3(0, -0.5F, 0);

            }
        }
    }

    bool checkIfPosEmpty(Vector3 targetPos) //et ei paneks kaste yksteise sisse, mis v6tab jube palju m2lu jube kiiresti...
    {
        targetPos.y = 1;
        if (Physics.CheckSphere(targetPos, 0.1F))
            return false;
        return true;
    }
}
