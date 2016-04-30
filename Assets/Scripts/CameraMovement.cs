using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    
    public float speed = 10.0f;// Nooltega liikumise kiirus
    public float zoomSpeed = 10.0f; // Rullikuga zoomimise kiirus
    public float minX_position = -20.0f;    // Kaamera positioni piirväärtused
    public float maxX_position = 20.0f;
    public float minY_position = 5.0f;
    public float maxY_position = 25.0f;
    public float minZ_position = -20.0f;
    public float maxZ_position = 20.0f;
    public float minX = -360.0f;    // Vasakule-paremale max nurk
    public float maxX = 360.0f;
    public float minY = -90.0f;    // Üles-alla max nurk
    public float maxY = 0.0f;
    public float sensX = 150.0f; // Kaamera rotationi muutmise tundlikkus
    public float sensY = 150.0f;
    float rotationY = 0.0f;     // Kaamera rotationi muutujate algväärtused (pole vaja muuta!)
    float rotationX = 90.0f;
    private bool esimenekord = true;
            //teha nii, et kaamera ei l2heks alguses vajutades kaugusse sihtima
            //nooltega(yles, alla) ka piirang ette y-telje suhtes; liiga awkward muidu liikuda.

    void Update(){

        float mwheelZoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.timeScale;
        transform.Translate(0, 0, mwheelZoom);

        if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetMouseButton(2))
        {
                rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
                rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
                rotationY = Mathf.Clamp(rotationY, minY, maxY);
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

        // Kaamera positioni piiramiseks
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX_position, maxX_position),
            Mathf.Clamp(transform.position.y, minY_position, maxY_position),
            Mathf.Clamp(transform.position.z, minZ_position, maxZ_position));
    }
}
