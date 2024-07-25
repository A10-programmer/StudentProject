using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float senX;
    public float senY;

    public Transform orientation;

    float XRotation;
    float YRotation;

    private static bool CanCameMove = true;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = 0;
        float mouseY = 0;
        if (CanCameMove)
        {
            mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
            mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;
        }
        //Get Mouse Input


        YRotation += mouseX;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -40f, 60f);


        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);

        orientation.rotation = Quaternion.Euler(0, YRotation, 0);
    }
    public static void PlayerCamerMoveEnabler()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerCamera.CanCameMove = true;
    }
    public static void PlayerCamerMoveDisabler()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerCamera.CanCameMove = false;
    }
}
