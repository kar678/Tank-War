using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SaveDialog;
    private int lastSaveSlot;
    
    public void PlayGame ()
    {
        SceneManager.LoadScene("TestWorld");
    }

    public void NewGame (int saveSlot)
    {
        if(SaveSystem.CheckFileExsits(saveSlot))
        {
            ShowSaveDialog();
            lastSaveSlot = saveSlot;
        }
        else
        {
            PlayerPrefs.SetInt("SaveSlot", saveSlot);
            SceneManager.LoadScene("Garage");
        }
    }

    public void LoadGame (int saveSlot)
    {
        PlayerPrefs.SetInt("SaveSlot", saveSlot);
        SceneManager.LoadScene("Garage");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    private void ShowSaveDialog()
    {
        SaveDialog.SetActive(true);
    }

    public void DialogYes()
    {
        SaveSystem.DeleteSaveFile(lastSaveSlot);
        PlayerPrefs.SetInt("SaveSlot", lastSaveSlot);
        SceneManager.LoadScene("Garage");
    }
}
