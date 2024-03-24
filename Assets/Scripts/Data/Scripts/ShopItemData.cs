using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "Data/ShopItemSO")]
public class ShopItemData : ScriptableObject
{
    public GameObject shopItem;
    public GameObject shopItemPack;
    public GameObject packItem;
    public string dialogDesc;
}
