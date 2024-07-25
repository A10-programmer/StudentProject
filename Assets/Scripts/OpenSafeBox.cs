using UnityEngine;

public class OpenSafeBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        StartSafeBoxCrack(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        StartSafeBoxCrack(collision);
    }
    private void StartSafeBoxCrack(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                UIController.instance.ShowSafeCracker(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
