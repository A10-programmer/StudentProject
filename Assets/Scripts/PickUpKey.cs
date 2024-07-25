using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PickupKey(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        PickupKey(collision);
    }
    private void PickupKey(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(UIController.instance != null)
                {
                    if (UIController.instance.Key1)
                    {
                        UIController.instance.Key2 = true;
                    }
                    else
                    {
                        UIController.instance.Key1 = true;
                    }
                }
                gameObject.SetActive(false);
            }
        }
    }
}
