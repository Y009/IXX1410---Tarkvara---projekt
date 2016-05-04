using UnityEngine;
using System.Collections;

public class exitgame : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip exitsnd;

    public void onClick()
    {
        Application.Quit();
    }
}
