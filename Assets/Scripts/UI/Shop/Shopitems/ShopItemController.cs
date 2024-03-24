using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController
{
    private ShopItemModel model;
    private GameObject dialogWindow;
    private GameObject currentItem;
    private Image currentImage;
    private RectTransform currentRect;
    private float timer = 0;
    private float delay = 0.2f;
    private float size = 0;
    public ShopItemController()
    {
        model = new ShopItemModel((ShopItemData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Data/SO/ShopItemSO.asset", typeof(ShopItemData)));
    }

    public void Updating()
    {
        if(currentItem != null && currentItem.activeInHierarchy)
        {
            timer += Time.deltaTime;

            if (timer >= delay)
            {
                timer = 0f;
                size += 8;
            }

            currentRect.sizeDelta = new Vector2(size, size);

            if (size > 90)
            {
                currentImage.color = Color.green;
            }

            if (size > 100)
            {
                currentItem.transform.parent.gameObject.SetActive(false);
                size = 0;
                currentImage.color = Color.red;
            }
        }
    }

    public void newShopItem(Transform parent, shopItem item)
    {
        if (item.items == null)
        {
            GameObject shopItem = GameObject.Instantiate(model.ShopItem, parent);
            UISetter setter = setComponents(shopItem, item);
            GameObject.Destroy(setter);
        }
        else
        {
            GameObject shopItem = GameObject.Instantiate(model.ShopItemPack, parent);
            UISetter setter = setComponents(shopItem, item);
            for (int i = 0; i < item.items.Length; i++)
            {
                GameObject miniItemPack = GameObject.Instantiate(model.MiniItemPack, setter.UIObj[3].transform);
                UISetter miniSetter = miniItemPack.GetComponent<UISetter>();
                miniSetter.UIObj[0].GetComponent<Text>().text = item.items[i].key;
                if(item.items[i].param == null)
                {
                    miniSetter.UIObj[1].GetComponent<Text>().text = item.items[i].amount.ToString();
                }
                else
                {
                    miniSetter.UIObj[1].GetComponent<Text>().text = item.items[i].param;
                }
                GameObject.Destroy(miniSetter);
            }
            GameObject.Destroy(setter);
        }
    }

    private UISetter setComponents(GameObject shopItem, shopItem item)
    {
        UISetter setter = shopItem.GetComponent<UISetter>();
        setter.UIObj[0].GetComponent<Text>().text = item.key;
        setter.UIObj[1].GetComponent<Text>().text = item.price;
        setter.UIObj[2].GetComponent<Text>().text = item.currency;
        shopItem.GetComponent<Button>().onClick.AddListener(() => setDialogWindow(setter.UIObj[4]));
        return setter;
    }

    private void buyItem()
    {
        Debug.Log("im Buyingggg");
        dialogWindow.transform.parent.gameObject.SetActive(false);
        currentItem.transform.parent.gameObject.SetActive(true);
        GameObject.Destroy(dialogWindow);
    }

    private void setDialogWindow(GameObject item)
    {
        if(size == 0)
        {
            dialogWindow = DialogWindowController.newDialogWindow(buyItem, model.desc);
            currentItem = item;
            currentImage = item.GetComponent<Image>();
            currentRect = item.GetComponent<RectTransform>();
        }
    }
}
