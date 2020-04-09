using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ReturnToGarage()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Garage");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
