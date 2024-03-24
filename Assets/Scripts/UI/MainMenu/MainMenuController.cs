using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenuController
{
    private MainMenuModel model;
    private ShopController shopController;
    private LoadingScreen loadingScreen;
    private LeaderBoardSaves leaderBoard;
    private GameObject dialog;
    private InputField nickname;
    public MainMenuController(LoadingScreen screen)
    {
        loadingScreen = screen;
        model = new MainMenuModel((MainMenuData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Data/SO/MainMenuSO.asset", typeof(MainMenuData)));
        leaderBoard = (LeaderBoardSaves)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Saves/Files/LeaderBoard.asset", typeof(LeaderBoardSaves));
        setBtn();
        shopController = new ShopController(model.Prefab);
    }

    public void Updating()
    {
        shopController.Updating();
    }

    private void setBtn()
    {
        model.PlayButton.onClick.AddListener(play);
        model.ShopButton.onClick.AddListener(shop);
    }

    private void play()
    {
        nickname = DialogWindowController.newTextDialogWindow(saveNick, "Please enter your nickname");
    }

    private void shop()
    {
        shopController.setActivated();
        model.Prefab.SetActive(false);
    }


    private void saveNick()
    {
        leaderBoard.Nickname = nickname.text;
        string path = Path.Combine("Assets/Scripts/Saves/Files/", "LeaderBoardData.json");
        var outputString = JsonUtility.ToJson(leaderBoard);
        File.WriteAllText(path, outputString);
        loadGame();
        GameObject.Destroy(dialog);
    }

    private void loadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.setNewData(((LoadingScreenData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Data/SO/LoadingScreenGameSO.asset", typeof(LoadingScreenData))));
        SceneManager.LoadScene(1);
    }
}
