using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPrefsText : MonoBehaviour
{
    [SerializeField] string key;

    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        int value = PlayerPrefs.GetInt(key);
        text.SetText(value.ToString());
    }

    [ContextMenu("Clear PlayerPref")]
    void Clear()
    {
        PlayerPrefs.DeleteKey(key);
    }
}
