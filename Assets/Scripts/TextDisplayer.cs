using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayer : MonoBehaviour
{
    public void ChangeText(object obj)
    {
        gameObject.GetComponent<TextMeshProUGUI>().SetText(obj.ToString());
    }
}
