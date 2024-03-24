using UnityEngine;

public class DialogWindowModel
{
    private GameObject info;
    private GameObject input;
    private GameObject confirm;
    public GameObject parent;

    public GameObject Info { get { return info; } }
    public GameObject Input { get { return input; } }
    public GameObject Confirm { get { return confirm; } }
    public DialogWindowModel(DialogWindowData data)
    {
        info = data.info;
        input = data.input;
        confirm = data.confirm;
        parent = GameObject.Instantiate(data.parent);

    }
}
