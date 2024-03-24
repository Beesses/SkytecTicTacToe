using UnityEngine;
using UnityEngine.UI;

public class MainMenuModel
{
    private Button playBtn;
    private Button shopBtn;
    private GameObject prefab;

    public Button PlayButton { get { return playBtn; } }
    public Button ShopButton { get { return shopBtn; } }
    public GameObject Prefab { get { return prefab; } }
    public MainMenuModel(MainMenuData data)
    {
        prefab = GameObject.Instantiate(data.prefab);
        UISetter setter = prefab.GetComponent<UISetter>();
        playBtn = setter.UIObj[0].GetComponent<Button>();
        shopBtn = setter.UIObj[1].GetComponent<Button>();
        GameContext.Destroy(setter);
    }
}
