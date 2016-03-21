using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{

    public AudioClip playsnd;
    bool paused = false;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
            paused = togglePause();
    }

    void OnGUI()
    {
        if (paused)
        {
            GUILayout.Label("Take 5 everybody!");
            if (GUILayout.Button("UNPAUSE"))
            {
                paused = togglePause();
                AudioSource.PlayClipAtPoint(playsnd, new Vector3(5, 1, 2));
            }
            else if (GUILayout.Button("Main Menu"))
                {
                    SceneManager.LoadScene("scene3", LoadSceneMode.Single);
                    System.Threading.Thread.Sleep(250);
                    Vector3 loc = GameObject.Find("Main Camera").transform.position + new Vector3(0, -1.5f, 0);
                    AudioSource.PlayClipAtPoint(playsnd, loc);
                    paused = togglePause();
                    SceneManager.SetActiveScene(SceneManager.GetActiveScene());
                }
            else if (GUILayout.Button("EXIT"));
                {
                  //AudioSource.PlayClipAtPoint(exitsnd, new Vector3(5, 1, 2));
                    Application.Quit();
                }
        }
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
}