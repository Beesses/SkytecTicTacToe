using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetter : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> UIObjects;

    public List<GameObject> UIObj { get { return UIObjects; } }

    public void destroying()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
