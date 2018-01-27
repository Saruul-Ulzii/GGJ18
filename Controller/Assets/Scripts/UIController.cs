using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public string MissionText;
    public string PlayerIndicator;

    public Text MissionTextComponent;
    public Text PlayerIndicatorComponent;
    public Button EngineButton;
    public Image ButtonCenterImage;

    public bool CallSuccess = true;
    public bool CallFail = true;

    public GameObject FailNotification;
    public GameObject SuccesNotification;

    public float NotificationWaitTime = 2.0f;
    // starting value for the Lerp
    static float t = 0.0f;

    private Vector3 NotificationStartPos;

    // Use this for initialization
    void Start()
    {
        MissionTextComponent.text = MissionText;
        PlayerIndicatorComponent.text = PlayerIndicator;
        UpdateColor();
        NotificationStartPos = SuccesNotification.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CallSuccess)
        {
            TriggerNotification(SuccesNotification);
        }
        if (CallFail)
        {
            TriggerNotification(FailNotification);
        }

        UpdateColor();
    }

    private void UpdateColor()
    {
        var colors = EngineButton.colors;
        colors.pressedColor = PlayerValues.Color;
        EngineButton.colors = colors;

        ButtonCenterImage.color = PlayerValues.Color;
    }

    private void TriggerNotification(GameObject notification)
    {
        notification.SetActive(true);
        // animate the position of the game object...
        Vector3 pos = NotificationStartPos;
        pos.y = Mathf.Lerp(pos.y + 100, pos.y, t);
        notification.transform.position = pos;

        // .. and increate the t interpolater
        t += 0.5f * Time.deltaTime;

        if (t > 1.0f)
        {
            t = 0;
            CallSuccess = false;
            CallFail = false;
            StartCoroutine(DeactivateNotifications(notification));
        }
    }

    private IEnumerator DeactivateNotifications(GameObject notification)
    {
        yield return new WaitForSeconds(NotificationWaitTime);
        notification.transform.position = NotificationStartPos;
        notification.SetActive(false);
    }
}
