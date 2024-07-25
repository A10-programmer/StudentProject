using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public List<Image> EkeyImageKey = new List<Image>();
    public List<Button> buttons_of_SafeCrack = new List<Button>();
    public List<TextMeshProUGUI> Texts_of_SafeCrack = new List<TextMeshProUGUI>();
    public GameObject KeysParent;
    public GameObject Player;
    [ReadOnly] public int first_Number_Of_pass;
    [ReadOnly] public int seceond_Number_Of_pass;
    [ReadOnly] public int third_Number_Of_pass;
    bool[] Passed_Numbers = { false, false, false };
    private void Start()
    {
        first_Number_Of_pass = Random.Range(0, 99);
        seceond_Number_Of_pass = Random.Range(first_Number_Of_pass, 0);
        third_Number_Of_pass = Random.Range(seceond_Number_Of_pass, 99);

        Transform[] children = new Transform[KeysParent.transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = KeysParent.transform.GetChild(i);
        }

        // Choose a random index
        int randomIndex = Random.Range(0, children.Length);

        // Activate the randomly chosen child
        children[randomIndex].gameObject.SetActive(true);

        for (int i = 0, j = 0; i < buttons_of_SafeCrack.Count && j < Texts_of_SafeCrack.Count; i += 2, j++)
        {
            int buttonIndex = i;
            int textIndex = j;
            buttons_of_SafeCrack[buttonIndex].onClick.AddListener(() => ChangeNumber(true, Texts_of_SafeCrack[textIndex]));
        }
        for (int i = 1, j = 0; i < buttons_of_SafeCrack.Count && j < Texts_of_SafeCrack.Count; i += 2, j++)
        {
            int buttonIndex = i;
            int textIndex = j;
            buttons_of_SafeCrack[buttonIndex].onClick.AddListener(() => ChangeNumber(false, Texts_of_SafeCrack[textIndex]));
        }
        buttons_of_SafeCrack[0].interactable = true;
        for (int i = 1; i < buttons_of_SafeCrack.Count; i++)
        {
            buttons_of_SafeCrack[i].interactable = false;
        }
    }
    // Define a field to store the current number
    private int currentNumber = 0;

    private void ChangeNumber(bool up, TextMeshProUGUI number_text)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SfxSoundsclipList[0]);
        if (up)
        {
            currentNumber = Mathf.Clamp(currentNumber + 1, 0, 99);
        }
        else
        {
            currentNumber = Mathf.Clamp(currentNumber - 1, 0, 99);
        }
        number_text.text = currentNumber.ToString();
    }
    private void Update()
    {
        for (int i = 0; i < EkeyImageKey.Count; i++)
        {
            if (EkeyImageKey[i] != null)
            {
                if (Vector3.Distance(EkeyImageKey[i].transform.parent.parent.position, Player.transform.position) < 2f)
                {
                    EkeyImageKey[i].gameObject.SetActive(true);
                }
                else
                {
                    EkeyImageKey[i].gameObject.SetActive(false);
                }
            }
        }
        if (int.Parse(Texts_of_SafeCrack[0].text) > 0 && !Passed_Numbers[0])
        {
            buttons_of_SafeCrack[1].interactable = true;
        }
        else
        {
            buttons_of_SafeCrack[1].interactable = false;
        }
        if (int.Parse(Texts_of_SafeCrack[1].text) < first_Number_Of_pass && !Passed_Numbers[1] && Passed_Numbers[0])
        {
            buttons_of_SafeCrack[2].interactable = true;
        }
        else
        {
            buttons_of_SafeCrack[2].interactable = false;
        }
        if (int.Parse(Texts_of_SafeCrack[2].text) > seceond_Number_Of_pass && !Passed_Numbers[2])
        {
            buttons_of_SafeCrack[5].interactable = true;
        }
        else
        {
            buttons_of_SafeCrack[5].interactable = false;
        }
        if (Texts_of_SafeCrack[0].text == first_Number_Of_pass.ToString() && !Passed_Numbers[0])
        {
            AudioManager.instance.sfxSource.Stop();
            AudioManager.instance.PlaySFX(AudioManager.instance.SfxSoundsclipList[1]);
        }
        if (Texts_of_SafeCrack[1].text == seceond_Number_Of_pass.ToString() && !Passed_Numbers[1])
        {
            AudioManager.instance.sfxSource.Stop();
            AudioManager.instance.PlaySFX(AudioManager.instance.SfxSoundsclipList[1]);
        }
        if (Texts_of_SafeCrack[2].text == third_Number_Of_pass.ToString() && !Passed_Numbers[2])
        {
            AudioManager.instance.sfxSource.Stop();
            AudioManager.instance.PlaySFX(AudioManager.instance.SfxSoundsclipList[1]);
        }
        if (UIController.instance.PlayerUI.transform.GetChild(3).gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Texts_of_SafeCrack[0].text == first_Number_Of_pass.ToString() && !Passed_Numbers[0])
                {
                    Passed_Numbers[0] = true;
                    buttons_of_SafeCrack[0].interactable = false;
                    buttons_of_SafeCrack[1].interactable = false;
                    buttons_of_SafeCrack[2].interactable = true;
                    buttons_of_SafeCrack[3].interactable = true;
                    Texts_of_SafeCrack[1].text = first_Number_Of_pass.ToString();
                }
                if (Texts_of_SafeCrack[1].text == seceond_Number_Of_pass.ToString() && !Passed_Numbers[1])
                {
                    Passed_Numbers[1] = true;
                    buttons_of_SafeCrack[2].interactable = false;
                    buttons_of_SafeCrack[3].interactable = false;
                    buttons_of_SafeCrack[4].interactable = true;
                    buttons_of_SafeCrack[5].interactable = true;
                    Texts_of_SafeCrack[2].text = seceond_Number_Of_pass.ToString();
                }
                if (Texts_of_SafeCrack[2].text == third_Number_Of_pass.ToString() && !Passed_Numbers[2])
                {
                    Passed_Numbers[2] = true;
                    buttons_of_SafeCrack[4].interactable = false;
                    buttons_of_SafeCrack[5].interactable = false;
                    UIController.instance.ShowSafeCracker(false);
                    UIController.instance.Key2 = true;
                }
            }
        }
    }
}
