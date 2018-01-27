using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Color PlayerColor;

    public string MissionText;

    public string PlayerIndicator;

    public bool CallSuccess = true;
    public bool CallFail = true;

    // starting value for the Lerp
    static float t = 0.0f;

    private float minimum = -100.0F;
    private float maximum = 100.0F;
    private Vector3 PositionNotifications;
    

    // Use this for initialization
    void Start()
    {
        //Set Texts
        transform.Find("MissionText").GetComponent<Text>().text = MissionText;
        transform.Find("EngineButton").GetComponentInChildren<Text>().text = PlayerIndicator;

        //Set Colors
        var colors = transform.Find("EngineButton").GetComponent<Button>().colors;
        colors.pressedColor = PlayerColor;
        transform.Find("EngineButton").GetComponent<Button>().colors = colors;

        transform.Find("EngineButton").transform.Find("Center").GetComponent<Image>().color =
            PlayerColor;

        //Position of Notifications
        PositionNotifications = transform.Find("Success").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CallSuccess)
        {
            triggerNotification("Success");
        }
        if (CallFail)
        {
            triggerNotification("Fail");
        }
    }


    private void triggerNotification(string notificationName)
    {
        transform.Find(notificationName).gameObject.SetActive(true);
        // animate the position of the game object...
        Vector3 pos = PositionNotifications;
        pos.y = Mathf.Lerp(pos.y-minimum, pos.y-maximum, t);
        transform.Find(notificationName).transform.position = pos;

        // .. and increate the t interpolater
        t += 0.5f * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if (t > 1.0f)
        {
            CallSuccess = false;
            CallFail = false;
            transform.Find(notificationName).transform.position = PositionNotifications;
        }
    }

}