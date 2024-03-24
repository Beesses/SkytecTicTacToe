using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindowController
{
    private static DialogWindowModel model;
    public DialogWindowController()
    {
        model = new DialogWindowModel((DialogWindowData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Data/SO/DialogWindowSO.asset", typeof(DialogWindowData)));
    }

    //Возможность в любой точке подтвердить действие
    public static GameObject newDialogWindow(Action action, string desc = "")
    {
        model.parent.SetActive(true);
        GameObject dialogWindow = GameObject.Instantiate(model.Confirm, model.parent.transform);
        UISetter setter = dialogWindow.GetComponent<UISetter>();
        setter.UIObj[0].GetComponent<Text>().text = desc;
        Button btn = setter.UIObj[1].GetComponent<Button>();
        btn.onClick.AddListener(action.Invoke);
        return dialogWindow;
    }

    //Возможность в любой точке получить ответ в качестве string
    public static InputField newTextDialogWindow(Action action, string desc = "")
    {
        model.parent.SetActive(true);
        GameObject dialogWindow = GameObject.Instantiate(model.Input, model.parent.transform);
        UISetter setter = dialogWindow.GetComponent<UISetter>();
        setter.UIObj[0].GetComponent<Text>().text = desc;
        InputField input = setter.UIObj[1].GetComponent<InputField>();
        Button btn = setter.UIObj[2].GetComponent<Button>();
        btn.onClick.AddListener(() => { action.Invoke(); GameObject.Destroy(dialogWindow); model.parent.SetActive(false); });
        return input;
    }
    //Можно так добавить инфо окно диалога.
}
