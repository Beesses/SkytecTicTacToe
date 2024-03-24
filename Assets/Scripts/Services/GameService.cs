public class GameService : BaseService
{
    private GameController controller;
    private DialogWindowController dialog;
    public GameService(GameContext context, LoadingScreen loading)
    {
        context.AddToList(this);
        dialog = new DialogWindowController();
        controller = new GameController(context, 3, loading);
    }
    public override void OnAwake()
    {

    }
    public override void OnUpdate()
    {
        controller.Updating();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnLateUpdate()
    {

    }

}
