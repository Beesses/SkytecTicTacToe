using System.IO;
using UnityEditor;
using UnityEngine;

public class ShopController
{
    private ShopModel model;
    private ShopItemController shopItemController;
    private string jsonpath = "Assets/Scripts/Data/SO/ShopItems.json";
    private GameObject menu;
    
    ShopItems data;

    public ShopController(GameObject Menu)
    {
        model = new ShopModel((ShopData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Data/SO/ShopSO.asset", typeof(ShopData)));
        shopItemController = new ShopItemController();
        menu = Menu;
        getJsonData();
        initShopItems();
        setBtn();
    }

    public void Updating()
    {
        shopItemController.Updating();
    }

    private void initShopItems()
    {
        for(int i = 0; i < data.shopItems.Length; i++)
        {
            shopItemController.newShopItem(model.ShopElements.transform, data.shopItems[i]);
        }
    }

    private void getJsonData()
    {
        try
        {
            string jsona = File.ReadAllText(jsonpath);
            data = JsonUtility.FromJson<ShopItems>(jsona);
        }
        catch
        {
            Debug.Log("File is empty");
        }
    }

    public void setActivated(bool active = true)
    {
        model.Prefab.SetActive(active);
    }

    private void backToMenu()
    {
        setActivated(false);
        menu.SetActive(true);
    }

    private void setBtn()
    {
        model.Button.onClick.AddListener(backToMenu);
    }
}

[System.Serializable]
public class ShopItems
{
    public shopItem[] shopItems;
}

[System.Serializable]
public class shopItem
{
    public string key;
    public packItem[] items;
    public int amount;
    public string price;
    public string currency;
}

[System.Serializable]
public class packItem
{
    public string key;
    public int amount;
    public string param;
}
