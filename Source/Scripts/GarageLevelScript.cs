using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GarageLevelScript : MonoBehaviour
{
    string selectedLevel = null;
	string tankPrefab = null;
	public Button missionsButton;
    public Button goButton;
    public GameObject info;
	string levelDisplayName = null;
	public TextMeshProUGUI missionText;
    public TextMeshProUGUI tankText;
    public Transform tankSpawnLocation;

    public void SetSelectedLevel(string Level)
    {
        selectedLevel = Level;
    }
	
	public void SetSelectedLevelDisplayName(string DisplayName)
    {
		levelDisplayName = DisplayName;
    }

    public void SetTankPrefab(string Tank)
    {
        GameObject preTank = GameObject.FindGameObjectWithTag("Player");
        if (preTank)
        {
            UnityEngine.Object.Destroy(preTank);
        }

        tankPrefab = Tank;
		PlayerPrefs.SetString("TankPrefab", Tank);
        string tankPath = "Tanks/" + Tank;
        GameObject TankPrefab = Resources.Load(tankPath, typeof(GameObject)) as GameObject;

        GameObject iTank = Instantiate(TankPrefab, tankSpawnLocation.position, tankSpawnLocation.rotation);

        TankController tc = iTank.GetComponent<TankController>();
        TankTurretController ttc = iTank.GetComponentInChildren<TankTurretController>();
        TankWeaponController twc = iTank.GetComponentInChildren<TankWeaponController>();
        tc.disableControls = true;
        ttc.disableControls = true;
        twc.disableControls = true;
        twc.inGarage = true;
    }

    public void SetTankDisplayName(string Name)
    {
        tankText.text = Name;
    }

    public void GoToBattle()
    {
        if (selectedLevel != null && tankPrefab != null)
        {
            SceneManager.LoadScene(selectedLevel);
        }
    }
	
	void FixedUpdate()
	{
		if (tankPrefab != null && missionsButton)
		{
			missionsButton.interactable = true;
			info.SetActive(false);
		}
		else if (missionsButton)
		{
			missionsButton.interactable = false;
			info.SetActive(true);
		}

        if (tankPrefab != null && selectedLevel != null && goButton)
        {
            goButton.interactable = true;
        }
        else if (goButton)
        {
            goButton.interactable = false;
        }

        if (missionText)
		{
			if(levelDisplayName != null)
			{
				missionText.text = "Mission: " + levelDisplayName;
			}
		}
	}
}
