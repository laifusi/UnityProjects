using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerMenu : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene("Level" + level);
    }
}
