using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    GameObject tankPrefab;
    public Transform spawnLocation;
    public bool spawnWithWeaponsDisabled = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetString("TankPrefab", "TankTest") != "No Tank")
        {
            string tankPath = "Tanks/" + PlayerPrefs.GetString("TankPrefab", "TankTest");
            tankPrefab = Resources.Load(tankPath, typeof(GameObject)) as GameObject;
            // Instantiate at position (0, 0, 0) and zero rotation.
            GameObject ints = Instantiate(tankPrefab, spawnLocation.position, spawnLocation.rotation);

            if(spawnWithWeaponsDisabled)
            {
                TankWeaponController twc = ints.GetComponentInChildren<TankWeaponController>();
                twc.disableControls = true;
            }
        }
    }
}
