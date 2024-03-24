using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen: MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;
    [SerializeField]
    private Text loadingTextUI;
    [SerializeField]
    private Text hintTextUI;
    private string[] loadingTexts;
    private string hintText;
    public float timerLenght;

    private void Awake()
    {
        setData(((LoadingScreenData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Data/SO/LoadingScreenSO.asset", typeof(LoadingScreenData))));
        StartCoroutine(loadNextScene());
        DontDestroyOnLoad(this);
    }

    private void setData(LoadingScreenData data)
    {
        timerLenght = data.timerLenght;
        loadingTexts = data.LoadingText;
        hintText = data.HintText;
    }

    public void setNewData(LoadingScreenData data)
    {
        timerLenght = data.timerLenght;
        loadingTexts = data.LoadingText;
        hintText = data.HintText;
        StartCoroutine(loadNextScene());
    }

    public IEnumerator loadNextScene()
    {
        hintTextUI.text = hintText;
        this.gameObject.SetActive(true);
        for (int i = 1; i < timerLenght + 1; i++)
        {
            loadingTextUI.text = loadingTexts[i - 1];
            loading(timerLenght, i);
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
        loadingBar.rectTransform.sizeDelta = new Vector2(0 / timerLenght, 90);
    }

    private void loading(float timerLenght, float timer = 0)
    {
        loadingBar.rectTransform.sizeDelta = new Vector2(timer * 1200 / timerLenght, 90);
    }
}
