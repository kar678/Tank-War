using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureZoneController : MonoBehaviour
{

    public Color enemyColour;
    public Color friendlyColour;
    public Color neutralColour;
    public int owningTeam = 0;
    private SpriteRenderer boxRenderer;
    public float totalCaptureTime = 60.0f;
    private float percentAddValue;
    private float capturePercent;
    private float maxCapturePercent;
    private int team0People = 0;
    private int team1People = 0;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        boxRenderer = GetComponent<SpriteRenderer>();

        float a = totalCaptureTime;
        percentAddValue = 100.0f / a;
        maxCapturePercent = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(owningTeam == 0)
        {
            boxRenderer.color = neutralColour;
        }
        else if (owningTeam == 1)
        {
            boxRenderer.color = friendlyColour;
        }
        else
        {
            boxRenderer.color = neutralColour;
        }
       
        if(team1People > team0People && capturePercent < maxCapturePercent)
        {
            capturePercent = (capturePercent < maxCapturePercent) ? capturePercent + percentAddValue * Time.deltaTime : maxCapturePercent;
        }

        if(capturePercent > maxCapturePercent)
        {
            capturePercent = maxCapturePercent;
        }

        if(capturePercent == maxCapturePercent)
        {
            owningTeam = 1;
        }

        if(slider)
        {
            slider.value = capturePercent;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            team1People = team1People + 1;
        }

        if (hitInfo.gameObject.tag == "Enemy")
        {
            team0People = team0People + 1;
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            team1People = team1People - 1;
        }

        if (hitInfo.gameObject.tag == "Enemy")
        {
            team0People = team0People - 1;
        }
    }
}
