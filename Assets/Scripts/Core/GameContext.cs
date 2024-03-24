using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameContext : MonoBehaviour
{
    private static GameContext gameContext;
    //List<IAwake> OnAwakeList = new List<IAwake>();
    List<IUpdate> OnUpdateList = new List<IUpdate>();
    List<IFixedUpdate> OnFixedUpdateList = new List<IFixedUpdate>();
    List<ILateUpdate> OnLateUpdateList = new List<ILateUpdate>();

    [SerializeField]
    private LoadingScreen loadingScreen;
    private static LoadingScreen contextLoadingScreen;

    //Усть возможность реализовать Awake, но я использовал в качестве него конструкторы
    public void AddToList(BaseService script)
    {
        //OnAwakeList.Add(script);
        OnUpdateList.Add(script);
        OnFixedUpdateList.Add(script);
        OnLateUpdateList.Add(script);
    }

    private void Awake()
    {
        if (gameContext == null)
        {
            gameContext = this;
        }
        else if (this != gameContext)
        {
            Destroy(gameObject);
        }

        if(gameContext == this)
        {
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }

        if (contextLoadingScreen == null)
        {
            contextLoadingScreen = loadingScreen;
        }
        else
        {
            Destroy(loadingScreen.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        foreach (var feature in OnUpdateList)
        {
            feature.OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach (var feature in OnFixedUpdateList)
        {
            feature.OnFixedUpdate();
        }
    }
    private void LateUpdate()
    {
        foreach (var feature in OnLateUpdateList)
        {
            feature.OnLateUpdate();
        }
    }

    private void MainMenuScene()
    {
        new UIService(this, contextLoadingScreen);
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            currentName = SceneManager.GetActiveScene().name;
        }
        if(currentName == "Game")
        {
            SceneChanged();
        }
        else if(currentName == "MainMenu")
        {
            MainMenuScene();
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);
    }

    private void SceneChanged()
    {
        new GameService(this, contextLoadingScreen);
    }
}
