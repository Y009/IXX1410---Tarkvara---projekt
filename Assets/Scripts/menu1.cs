using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu1 : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip playsnd;
    public void onClick()
    {
        SceneManager.LoadScene("scene1", LoadSceneMode.Single);
        System.Threading.Thread.Sleep(250);
        Vector3 loc = GameObject.Find("Main Camera").transform.position + new Vector3(0, -1.5f, 0);
        AudioSource.PlayClipAtPoint(playsnd, loc);
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
}