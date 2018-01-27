using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class AddressQrReader : MonoBehaviour {



	private WebCamTexture camTexture;
	private Rect screenRect;

	public VerticalWebSocket Ws;
	public GameObject Next;

    public Int32 counter;

	void Start() 
	{
		screenRect = new Rect(0, 0, Screen.width, Screen.height);
		camTexture = new WebCamTexture();
		camTexture.requestedHeight = Screen.height; 
		camTexture.requestedWidth = Screen.width;
        counter = 0;
		if (camTexture != null) 
		{
			camTexture.Play();
		}
	}

	void OnGUI () {
		// drawing the camera on screen
		GUI.DrawTexture (screenRect, camTexture, ScaleMode.ScaleToFit);
        counter += 1;
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        if (counter % 10 == 0)
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(camTexture.GetPixels32(),
                camTexture.width, camTexture.height);
                if (result != null)
                {
                    Next.SetActive(true);
                    gameObject.SetActive(false);
                    Ws.connect(result.Text);
                }

            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
            }
        }
        
	}
}
