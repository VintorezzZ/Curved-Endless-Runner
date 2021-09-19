using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FormBoardToMenu : MonoBehaviour
{
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void GoLeaderboard()
    {
        SceneManager.LoadScene("Highscore");
    }
}
