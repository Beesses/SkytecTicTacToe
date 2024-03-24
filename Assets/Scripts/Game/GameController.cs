using System.Collections;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Не хватило времени отрефакторить GameController потому что пару дней выпало
public class GameController
{
    private GameModel[,] model;
    private BotController bot;
    private GameContext context;
    private LoadingScreen loadingScreen;
    private LeaderBoardSaves leaderBoard;
    private GameObject dialog;
    //в GameModel
    private GameObject pauseCanvas;
    //в GameModel
    private Text scoreText;
    //в GameModel
    private Text playerNicknameText;
    //в ScoreController
    private int score;
    //в FieldController
    private bool isFirstPlayer = true;
    //в FieldController
    private bool isFirstTurn = true;
    //в FieldController
    private bool isReload = false;
    private bool isPause = false;
    //в FieldController
    private int[] previousTurn;
    //в GameModel
    private GameObject parent = null;
    private int size;

    public GameController(GameContext gameContext, int fieldSize, LoadingScreen loading)
    {
        loadingScreen = loading;
        context = gameContext;
        leaderBoard = (LeaderBoardSaves)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Saves/Files/LeaderBoard.asset", typeof(LeaderBoardSaves));
        model = new GameModel[fieldSize, fieldSize];
        bot = new BotController();
        //Можно создать поле любого размера и бот отыграет нормально
        //А так можно будет проверить свою победу
        //Но не стал делать корректировку ячеек под количество полей поэтому фичу не реализовал
        //Потому что уже в целом спешил
        setField(fieldSize);
        playerNicknameText.text = leaderBoard.Nickname;
        size = fieldSize;
        if(isFirstPlayer == true)
        {
            isFirstTurn = true;
        }
        else
        {
            isFirstTurn = false;
        }
    }

    public void Updating()
    {
        if (isPause)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(true);
            isPause = true;
        }

        if (isReload)
        {
            return;
        }
        if (checkSpaceField() == true && isFirstTurn == false)
        {
            if(isFirstPlayer == true)
            {
                if (previousTurn == null)
                {
                    previousTurn = new int[] { Random.Range(0, 2), Random.Range(0, 2) };
                }
                int[] checkBot = bot.makeTurn(previousTurn[0], previousTurn[1], model, size, !isFirstPlayer);
                check(checkBot[0], checkBot[1]);
                isFirstTurn = !isFirstTurn;
            }
            else
            {
                if (previousTurn == null)
                {
                    previousTurn = new int[] { Random.Range(0, 2), Random.Range(0, 2) };
                }
                int[] checkBot = bot.makeTurn(previousTurn[0], previousTurn[1], model, size, !isFirstPlayer);
                check(checkBot[0], checkBot[1]);
                isFirstTurn = !isFirstTurn;
            }
        }
    }

    //в FieldController
    IEnumerator reloadField()
    {
        isReload = true;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                model[i, j].Btn.enabled = false;
            }
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject.Destroy(model[i, j].Prefab);
            }
        }
        isFirstPlayer = !isFirstPlayer;
        if (isFirstPlayer == true)
        {
            isFirstTurn = true;
        }
        else
        {
            isFirstTurn = false;
        }
        setField(size);
    }

    //в FieldController
    private void setField(int size)
    {
        DataGameModel data = ((DataGameModel)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Data/SO/GameSO.asset", typeof(DataGameModel)));
        if(parent == null)
        {
            UISetter setter = GameObject.Instantiate(data.canvas).GetComponent<UISetter>();
            parent = setter.UIObj[0];
            playerNicknameText = setter.UIObj[1].GetComponent<Text>();
            scoreText = setter.UIObj[2].GetComponent<Text>();
            GameObject.Destroy(setter);
            pauseCanvas = GameObject.Instantiate(data.pauseCanvas);
            setter = pauseCanvas.GetComponent<UISetter>();
            setter.UIObj[0].GetComponent<Button>().onClick.AddListener(() => { pauseCanvas.SetActive(false); isPause = false; });
            setter.UIObj[1].GetComponent<Button>().onClick.AddListener(exit);
        }
        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                model[i,j] = new GameModel(data, parent.transform);
                GameModel modeltest = model[i, j];
                int row = i;
                int col = j;
                model[i,j].Btn.onClick.AddListener(() => setBtn(modeltest.Symbol, row, col));
            }
        }
        isReload = false;
    }

    //в FieldController
    private void setBtn(Text text, int x, int y)
    {
        if(checkSpaceField() == false)
        {
            return;
        }
        if (isFirstPlayer && isFirstTurn)
        {
            model[x, y].test = 1;
            text.text = "X";
            isFirstTurn = !isFirstTurn;
            model[x, y].Btn.enabled = false;
            previousTurn = new int[] { x, y };
            check(x, y);
        }
        else if(!isFirstPlayer)
        {
            model[x,y].test = 2;
            text.text = "O";
            isFirstTurn = !isFirstTurn;
            model[x, y].Btn.enabled = false;
            previousTurn = new int[] { x, y };
            check(x, y);
        }
    }

    //в FieldController
    private bool checkSpaceField()
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if(model[i,j].test == 0)
                {
                    return true;
                }
            }
        }
        context.StartCoroutine(reloadField());
        return false;
    }

    //в FieldController
    private void check(int x, int y)
    {
        int value = model[x, y].test;
        int checkwin = 0;

        for (int j = 0; j < size; j++)
        {
            if (model[x, j].test == value)
            {
                checkwin += value;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 3 || checkwin == 6)
            {
                setScore(checkwin);
            }
        }

        checkwin = 0;
        for (int j = 0; j < size; j++)
        {
            if (model[j, y].test == value)
            {
                checkwin += value;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 3 || checkwin == 6)
            {
                setScore(checkwin);
            }
        }
        int row = x;
        int col = y;

        int g = y;

        for (int i = x; i >= 0 && g < size; i--, g++)
        {
            row = i;
            col = g;
        }

        checkwin = 0;
        g = col;
        for (int i = row; i < size && g >= 0; i++, g--)
        {
            if (model[i, g].test == value)
            {
                checkwin += value;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 3 || checkwin == 6)
            {
                setScore(checkwin);
            }
            col++;
        }


        if (x > y)
        {
            row = x - y;
            col = 0;
        }
        else if (x < y)
        {
            col = y - x;
            row = 0;
        }
        else
        {
            row = 0;
            col = 0;
        }
        checkwin = 0;
        for (int i = row; i < size && col < size; i++, col++)
        {
            if (model[i, col].test == value)
            {
                checkwin += value;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 3 || checkwin == 6)
            {
                setScore(checkwin);
            }
        }
    }

    //Надо было переместить в ScoreController
    private void setScore(int checkwin)
    {
        Debug.Log(isFirstPlayer);
        Debug.Log(checkwin);
        if (checkwin == 3)
        {
            if (isFirstPlayer)
            {
                score += 100;
            }
            else
            {
                score -= 100;
            }
            context.StartCoroutine(reloadField());
        }
        else if (checkwin == 6)
        {
            if (!isFirstPlayer)
            {
                score += 100;
            }
            else
            {
                score -= 100;
            }
            context.StartCoroutine(reloadField());
        }
        updateScore();
    }

    //Надо было переместить в ScoreController
    private void updateScore()
    {
        scoreText.text = score.ToString();
    }

    private void exit()
    {
        dialog = DialogWindowController.newDialogWindow(loadMenu, "Are you sure you wanna exit?");
    }

    private void loadMenu()
    {
        saveResult();
        loadingScreen.gameObject.SetActive(true);
        dialog.transform.parent.gameObject.SetActive(false);
        GameObject.Destroy(dialog);
        loadingScreen.setNewData(((LoadingScreenData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Data/SO/LoadingScreenSO.asset", typeof(LoadingScreenData))));
        SceneManager.LoadScene(0);
    }

    private void saveResult()
    {
        leaderBoard.LeaderDatas = leaderBoard.LeaderDatas.OrderByDescending(x => x.Score).ToList();
        foreach (var leader in leaderBoard.LeaderDatas)
        {
            Debug.Log(score);
            if (score > leader.Score)
            {
                leaderBoard.LeaderDatas.RemoveAt(8);
                LeaderData newleader;
                newleader.Name = leaderBoard.Nickname;
                newleader.Score = score;
                leaderBoard.LeaderDatas.Add(newleader);
                leaderBoard.CheckLoad = 1;
                leaderBoard.LeaderDatas = leaderBoard.LeaderDatas.OrderByDescending(x => x.Score).ToList();
                string path = Path.Combine("Assets/Scripts/Saves/Files/", "LeaderBoardData.json");
                var outputString = JsonUtility.ToJson(leaderBoard);
                Debug.Log(path);
                File.WriteAllText(path, outputString);
                Debug.Log("Score saved");
                break;
            }
        }
    }
}
