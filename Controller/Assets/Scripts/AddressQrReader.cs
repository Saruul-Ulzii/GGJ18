using System;
using System.Collections;
using UnityEngine;
using ZXing;
using UnityEngine.UI;

public class AddressQrReader : MonoBehaviour {



	private WebCamTexture camTexture;
	private Rect screenRect;

	public VerticalWebSocket Ws;
	public GameObject Next;
    public Image webcamDisplay;

    private Int32 counter;


    void Start() 
	{
		screenRect = new Rect(0, 0, Screen.width, Screen.height);
		camTexture = new WebCamTexture();
		camTexture.requestedHeight = Screen.height; 
		camTexture.requestedWidth = Screen.width;
        counter = 0;

        if (camTexture != null) 
		{
            coroutine_LowerFramerateForSeconds(1);
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

    private void Update()
    {
        if(camTexture == null)
        {
            return;
        }

        Texture2D tex = new Texture2D(camTexture.width, camTexture.height);
        tex.SetPixels32(camTexture.GetPixels32());

        var sprite = Sprite.Create(tex, new Rect(0, 0, camTexture.width, camTexture.height), Vector2.zero);
        webcamDisplay.sprite = sprite;
    }

    private IEnumerator coroutine_LowerFramerateForSeconds( float seconds)
    {
        int previous = Application.targetFrameRate;
        Application.targetFrameRate = 30;

        yield return new WaitForSeconds(seconds);
        if (Application.targetFrameRate == 30)
        {
            Application.targetFrameRate = previous; 
        }
    }
}
