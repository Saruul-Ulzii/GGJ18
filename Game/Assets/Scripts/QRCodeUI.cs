using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRCodeUI : MonoBehaviour
{
    private string ServerUrl = null;
    private Texture2D QrCode = null;

    public Image ImageElement;

    private void Update()
    {
        if(ServerUrl == null && Server.ServerUrl != null)
        {
            ServerUrl = Server.ServerUrl;
            var qrCode = GenerateBarcode.GenerateQR(ServerUrl);
            QrCode = qrCode;
            var sprite = Sprite.Create(QrCode, new Rect(0, 0, QrCode.width, QrCode.height), Vector2.zero);
            if (ImageElement) ImageElement.sprite = sprite;
        }
    }
}
