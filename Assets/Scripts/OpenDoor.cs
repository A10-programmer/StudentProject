using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 5f)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        opendoor(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        opendoor(collision);
    }
    private void opendoor(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (gameObject.CompareTag("ManagerDoor"))
                {
                    if (UIController.instance.Key1)
                        gameObject.SetActive(false);
                    else
                        UIController.instance.ShowText("You do not have the key to this door");
                }
                else if (gameObject.CompareTag("ExitDoor"))
                {
                    if (UIController.instance.Key2)
                        gameObject.SetActive(false);
                    else
                        UIController.instance.ShowText("You do not have the key to this door");
                }
            }
        }
    }
}
