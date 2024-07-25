using UnityEngine;

public class WinnerShow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIController.instance.showWinnerMenu();
        }
    }
}
