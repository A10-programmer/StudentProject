using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 3.5f)
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
        PowerUphandler(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        PowerUphandler(collision);
    }
    private void PowerUphandler(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (gameObject.tag == "HealthPow")
                {
                    if(PlayerMovment.Health < 100)
                    {
                        PlayerMovment.Health += 10;
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        UIController.instance.ShowText("Your health is full");
                    }
                }
                if (gameObject.tag == "InvPow")
                {
                    if(Monster_AI.HavAccessSeeplayer)
                    {
                        UIController.instance.StartCoroutine(EnableHavAccessSeeplayer());
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        UIController.instance.ShowText("The monster is currently unable to see you");
                    }
                }
            }
        }
    }
    IEnumerator EnableHavAccessSeeplayer()
    {
        Monster_AI.HavAccessSeeplayer = false;
        UIController.instance.Inv = true;
        UIController.instance.ShowText("You are invisible for one minute");
        for(int i  = 60; i > 0; i--)
        {
            UIController.instance.Inv_text.SetText(i.ToString());
            yield return new WaitForSeconds(1f);
        }

        UIController.instance.ShowText("You became visible");
        UIController.instance.Inv = false;
        Monster_AI.HavAccessSeeplayer = true;
    }
}
