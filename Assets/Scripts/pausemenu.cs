using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    //buggine pause menu
    public AudioClip playsnd;
    bool paused = false;
	public GUISkin GUI_1;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
            paused = togglePause();
    }

    void OnGUI()
    {
		GUI.skin = GUI_1;
        if (paused)
        {
			GUI.Box(new Rect(0, 0, 140, 180), "");
            GUI.Label(new Rect(20, 10, 100, 30), "Game is paused");
            if (GUI.Button(new Rect (10,45,120,37.5f), "UNPAUSE"))
            {
                paused = togglePause();
                AudioSource.PlayClipAtPoint(playsnd, new Vector3(5, 1, 2));
            }
            else if (GUI.Button(new Rect (10,82.5f,120,37.5f), "MAIN MENU"))
                {
                    SceneManager.LoadScene("scene3", LoadSceneMode.Single);
                    System.Threading.Thread.Sleep(250);         //.sleep ei ole v2ga 6ige kasutada, vaja mingi aeg v2lja vahetada
                    Vector3 loc = GameObject.Find("Main Camera").transform.position + new Vector3(0, -1.5f, 0);
                    AudioSource.PlayClipAtPoint(playsnd, loc);
                    paused = togglePause();
                    SceneManager.SetActiveScene(SceneManager.GetActiveScene());
                }
            else if (GUI.Button(new Rect (10,120,120,37.5f), "EXIT"))
                {
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