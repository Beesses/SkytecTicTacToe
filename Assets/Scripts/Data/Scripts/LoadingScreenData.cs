using UnityEngine;

[CreateAssetMenu(fileName = "LoadingScreenSO", menuName = "Data/LoadingSO")]
public class LoadingScreenData : ScriptableObject
{
    public string[] LoadingText;
    public string HintText;
    public int timerLenght;
    public GameObject prefab;
}
