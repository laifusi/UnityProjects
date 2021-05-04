using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHighscore : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().SetText(PlayerPrefs.GetInt("Highscore").ToString());
    }

    [ContextMenu("ResetHighscore")]
    void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("Highscore");
    }
}
