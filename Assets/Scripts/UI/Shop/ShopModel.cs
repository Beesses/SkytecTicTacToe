using UnityEngine;
using UnityEngine.UI;

public class ShopModel
{
    private GameObject prefab;
    private GameObject shopElements;
    private Button button;

    public GameObject ShopElements { get { return shopElements; } }
    public GameObject Prefab { get { return prefab; } }
    public Button Button{ get { return button; } }
    public ShopModel(ShopData data)
    {
        prefab = GameObject.Instantiate(data.prefab);
        UISetter setter = prefab.GetComponent<UISetter>();
        shopElements = setter.UIObj[0];
        button = setter.UIObj[1].GetComponent<Button>();
        GameObject.Destroy(setter);
    }
}
