using UnityEngine;

public class ShopItemModel
{
    private GameObject shopItem;
    private GameObject shopItemPack;
    private GameObject miniItemPack;
    public string desc;

    public GameObject ShopItem { get { return shopItem; } }
    public GameObject ShopItemPack { get { return shopItemPack; } }
    public GameObject MiniItemPack { get { return miniItemPack; } }

    public ShopItemModel(ShopItemData data)
    {
        shopItem = data.shopItem;
        shopItemPack = data.shopItemPack;
        miniItemPack = data.packItem;
        desc = data.dialogDesc;
    }
}
