using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenModel
{
    private Image loadingBar;
    private Text loadingTextUI;
    private Text hintTextUI;
    private GameObject prefab;
    private string[] loadingTexts;
    private string hintText;
    public float timerLenght;

    public Image LoadingBar { get { return loadingBar; } }
    public Text LoadingTextUI { get { return loadingTextUI; } }
    public Text HintTextUI { get { return hintTextUI; } }
    public GameObject Prefab { get { return prefab; } }
    public string[] LoadingText { get { return loadingTexts; } }
    public string HintText { get { return hintText; } }

    public LoadingScreenModel(LoadingScreenData data)
    {
        timerLenght = data.timerLenght;
        loadingTexts = data.LoadingText;
        hintText = data.HintText;
        prefab = GameObject.Instantiate(data.prefab);
        UISetter setter = prefab.GetComponent<UISetter>();
        loadingBar = setter.UIObj[0].GetComponent<Image>();
        loadingTextUI = setter.UIObj[1].GetComponent<Text>();
        hintTextUI = setter.UIObj[2].GetComponent<Text>();
        GameObject.Destroy(setter);
    }

    public void setNewData(LoadingScreenData data)
    {
        timerLenght = data.timerLenght;
        loadingTexts = data.LoadingText;
        hintText = data.HintText;
    }
}
