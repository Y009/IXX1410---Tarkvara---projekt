using UnityEngine;
using System.Collections;

public class exitgame : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip exitsnd;

    public void onClick()
    {
        Vector3 loc = GameObject.Find("Main Camera").transform.position + new Vector3(0, -1.5f, 0);
        AudioSource.PlayClipAtPoint(exitsnd, loc);
        Application.Quit();
    }
}
