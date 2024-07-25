using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AsyncOperation = UnityEngine.AsyncOperation;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject LoaderCanvas;
    public Image ProgressBar;
    public TextMeshProUGUI ProgressBarPercent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadScene(int SceneIndex)
    {
        StopAllCoroutines();
        StartCoroutine(LoadAsync(SceneIndex));
    }

    private IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);

        LoaderCanvas.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            ProgressBar.fillAmount = progress;
            ProgressBarPercent.text = progress * 100f + "%";
            yield return null;
        }
        if (SceneIndex == 1)
        {
            AudioManager.instance.musicSource.Stop();
            AudioManager.instance.PlayRain(AudioManager.instance.backgroundSoundsclipList[1]);
            PlayerMovment.Dead = false;
            PlayerMovment.Health = 100;
            UIController.instance.inLoseMenue = false;
            PlayerCamera.PlayerCamerMoveEnabler();
            UIController.instance.GameIsPause = false;
            UIController.instance.Inv = false;
            UIController.instance.Key1 = false;
            UIController.instance.Key2 = false;
            UIController.instance.StopAllCoroutines();
            StartCoroutine(UIController.instance.ShowHealth());
            Monster_AI.HavAccessSeeplayer = true;
        }
        else if (SceneIndex == 0)
        {
            UIController.instance.GameIsPause = false;
            AudioManager.instance.RainSource.Stop();
            AudioManager.instance.PlayMusic(AudioManager.instance.backgroundSoundsclipList[0]);
        }
        LoaderCanvas.SetActive(false);
    }
}
