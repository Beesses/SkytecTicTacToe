using UnityEngine;

[CreateAssetMenu(fileName = "DialogWindowSO", menuName = "Data/DialogWindowSO")]
public class DialogWindowData : ScriptableObject
{
    public GameObject info;
    public GameObject input;
    public GameObject confirm;
    public GameObject parent;
}
