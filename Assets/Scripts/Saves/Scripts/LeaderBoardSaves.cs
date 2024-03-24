using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderBoard", menuName = "Saves/LeaderBoard", order = 1)]
[System.Serializable]
public class LeaderBoardSaves: ScriptableObject
{
    public string Nickname;
    public GameObject prefab;
    public GameObject Results;
    public List<LeaderData> LeaderDatas = new List<LeaderData>();
    public int CheckLoad = 0;
}
