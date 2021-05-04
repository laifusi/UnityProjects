using UnityEngine;

public class UILockable : MonoBehaviour
{ 
    void OnEnable()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.LevelName + "Unlocked";
        int unlocked = PlayerPrefs.GetInt(key);
        if (unlocked == 0)
            gameObject.SetActive(false);
    }

    [ContextMenu("Clear Unlocked Level")]
    void ClearUnlockedLevel()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.LevelName + "Unlocked";
        PlayerPrefs.DeleteKey(key);
    }
}