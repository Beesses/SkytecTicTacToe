using UnityEngine;

[CreateAssetMenu(fileName = "GameSO", menuName = "Data/GameSO")]
public class DataGameModel : ScriptableObject
{
    public GameObject prefab;
    public GameObject canvas;
    public GameObject pauseCanvas;
}
