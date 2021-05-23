using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public string Label;

    // Start is called before the first frame update
    void Start()
    {
        Label = gameObject.GetComponentInChildren<TextMeshPro>().text;
    }
}
