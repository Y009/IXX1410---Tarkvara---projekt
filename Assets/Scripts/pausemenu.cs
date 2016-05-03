using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pausemenu : MonoBehaviour
{
    //buggine pause menu
    public AudioClip playsnd;
    bool paused = false;
	public GUISkin GUI_1;
	float w = (Screen.width/2);
	float h = (Screen.height/2);

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
			GUILayout.BeginArea (new Rect (w-160, h-180, 320, 360));
				GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace ();
					GUI.Box(new Rect(0, 0, 320, 360), "Game is paused");
					if (GUI.Button(new Rect (60, 90, 200, 60), "Unpause")){
                		paused = togglePause();
                		AudioSource.PlayClipAtPoint(playsnd, new Vector3(5, 1, 2));
					} else if (GUI.Button(new Rect (60, 180, 200, 60), "Main Menu")){
						SceneManager.LoadScene("scene3", LoadSceneMode.Single);
						System.Threading.Thread.Sleep(250);         //.sleep ei ole v2ga 6ige kasutada, vaja mingi aeg v2lja vahetada
						Vector3 loc = GameObject.Find("Main Camera").transform.position + new Vector3(0, -1.5f, 0);
						AudioSource.PlayClipAtPoint(playsnd, loc);
						paused = togglePause();
						SceneManager.SetActiveScene(SceneManager.GetActiveScene());
					} else if (GUI.Button(new Rect (60, 270, 200, 60), "Exit")){
						Application.Quit();
					}
					GUILayout.FlexibleSpace ();
				GUILayout.EndHorizontal();
			GUILayout.EndArea();
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