using UnityEngine;
using System.Collections;

public class globalscript : MonoBehaviour {

    public int skoor;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
