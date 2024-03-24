using UnityEngine;
using UnityEngine.UI;

public class GameModel
{
    private GameObject prefab;
    private Button btn;
    private Text symbol;
    public int test;

    public Text Symbol { get { return symbol; } }
    public Button Btn { get { return btn; } }
    public GameObject Prefab { get { return prefab; } }

    public GameModel(DataGameModel data, Transform parent)
    {
        prefab = GameObject.Instantiate(data.prefab, parent);
        btn = prefab.GetComponent<Button>();
        symbol = prefab.transform.GetChild(0).gameObject.GetComponent<Text>();
    }
}
