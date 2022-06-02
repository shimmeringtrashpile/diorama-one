using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    [SerializeField]
    ARSession m_Session;

    [SerializeField]
    TextMeshProUGUI ResponseText;

    [SerializeField]
    RenderTexture Render;

    [SerializeField]
    RawImage Background;

    [SerializeField]
    GameObject NewCube;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ARDelay());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRenderTexture();
        UpdateTestCube();
    }

    void UpdateTestCube()
    {
        WebCamTexture webCam = new WebCamTexture();
        Renderer renderer = NewCube.GetComponent<Renderer>();
        renderer.material.mainTexture = webCam;
        webCam.Play();
    }


    void UpdateRenderTexture()
    {

        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (WebCamDevice webCamDevice in devices)
        {
            print("webcam" + webCamDevice.name);

        }
        
        WebCamTexture text = new WebCamTexture(devices[0].name);
        Background.texture = text;
        //Background.;


        var commandBuffer = new CommandBuffer();
        commandBuffer.name = "AR Camera Background Blit Pass";
        // var texture = !m_ArCameraBackground.material.HasProperty("_MainTex") ? null : m_ArCameraBackground.material.GetTexture("_MainTex");
        Graphics.SetRenderTarget(Render.colorBuffer, Render.depthBuffer);
        commandBuffer.ClearRenderTarget(true, false, Color.clear);
        commandBuffer.Blit(Render, BuiltinRenderTextureType.CurrentActive, Background.material);
        Graphics.ExecuteCommandBuffer(commandBuffer);

    }


    IEnumerator ARDelay()
    {
        yield return new WaitForSeconds(.1f);
        if ((ARSession.state == ARSessionState.None) ||
            (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            // Start some fallback experience for unsupported devices
            ResponseText.text = "something";
        }
        else
        {
            // Start the AR session
            ResponseText.text = "something else";
            m_Session.enabled = true;
        }
    }
}
