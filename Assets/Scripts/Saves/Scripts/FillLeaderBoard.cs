using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FillLeaderBoard : MonoBehaviour
{
    [SerializeField]
    private LeaderBoardSaves _leaderBoard;
    private void Awake()
    {
        _leaderBoard = (LeaderBoardSaves)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Saves/Files/LeaderBoard.asset", typeof(LeaderBoardSaves));
        if (_leaderBoard.CheckLoad == 0)
        {
            try
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText(Path.Combine("Assets/Scripts/Saves/Files/", "LeaderBoardData.json")), _leaderBoard);
            }
            catch
            {
                Debug.Log("File is empty");
            }
        }

        Debug.Log(_leaderBoard);
        int i = 0;
        foreach(var leader in _leaderBoard.LeaderDatas)
        {
            //Debug.Log(leader.Score);
            new BoardModel(_leaderBoard.prefab, gameObject, leader, i);
            i++;
        }
        gameObject.transform.GetChild(0).SetAsLastSibling();
    }
}
