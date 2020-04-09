using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class TutorialHUDController : MonoBehaviour
{
    public GameObject hintPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI hintText;
    public Button buttonToContinue;
    InGameUIController playerHUD;
    bool showingHint = false;

    public AudioMixer masterMixer;

    void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameUIController>();
    }

    public void Continue()
    {
        showingHint = false;
        hintPanel.SetActive(false);
        buttonToContinue.gameObject.SetActive(false);

        Time.timeScale = 1;
        playerHUD.tutorialUI = false;
        masterMixer.SetFloat("GameVol", 0);
    }

    public void ShowHint(string Title ,string hint)
    {
        showingHint = true;
        titleText.text = Title;
        hintText.text = hint;

        hintPanel.SetActive(true);
        buttonToContinue.gameObject.SetActive(true);

        Time.timeScale = 0;
        playerHUD.tutorialUI = true;
        masterMixer.SetFloat("GameVol", -80);
    }
}
