using UnityEngine;
using UnityEngine.UI;

public class BoardModel
{
    public BoardModel(GameObject prefab, GameObject parent, LeaderData leaderData, int index)
    {
        GameObject.Instantiate(prefab, parent.transform);
        UISetter setter = prefab.GetComponent<UISetter>();
        setter.UIObj[0].GetComponent<Text>().text = leaderData.Name;
        setter.UIObj[1].GetComponent<Text>().text = leaderData.Score.ToString();
        setter.UIObj[2].GetComponent<Text>().text = (index + 1).ToString();
    }
}
