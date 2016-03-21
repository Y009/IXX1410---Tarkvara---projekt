using UnityEngine;
using System.Collections;

public class build_map11 : MonoBehaviour
{

    public GameObject buildobj;
    public GameObject slab;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha9))
            clearplane();
        else if (Input.GetKey(KeyCode.Alpha8))
            drawmap();
    }

    void clearplane()
    {
        GameObject[] koik = GameObject.FindGameObjectsWithTag("Plane");
        foreach (GameObject i in koik)
            Destroy(i);
    }

    void drawmap()
    {
        // teha check, et vaadata kas muudab juba eksiteerivat maze'i checifposemptyíga
        int row = 32;
        int column = 32;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject uus = (GameObject)Instantiate(buildobj, new Vector3(-15.5F + i, 0.51F, -15.5F + j), Quaternion.identity);
                GameObject uus2 = (GameObject)Instantiate(slab, new Vector3(-15.5F + i, 0.01F, -15.5F + j), Quaternion.identity);

            }
        }
    }

    bool checkIfPosEmpty(Vector3 targetPos) //et ei paneks kaste yksteise sisse, mis v6tab jube palju m2lu jube kiiresti...
    {
        if (Physics.CheckSphere(targetPos, 0.1F))
            return false;
        return true;
    }
}