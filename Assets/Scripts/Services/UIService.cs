
public class UIService : BaseService
{
    private MainMenuController mainMenuController;
    private LoadingScreen loadingScreen;
    private DialogWindowController dialogWindowController;
    public UIService(GameContext context, LoadingScreen screen)
    {
        context.AddToList(this);
        loadingScreen = screen;
        dialogWindowController = new DialogWindowController();
        mainMenuController = new MainMenuController(screen);
    }

    public override void OnAwake()
    {

    }

    public override void OnUpdate()
    {
        mainMenuController.Updating();
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnLateUpdate()
    {

    }
}
