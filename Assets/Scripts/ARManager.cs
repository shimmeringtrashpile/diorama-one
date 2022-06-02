using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    [SerializeField]
    ARSession m_Session;

    [SerializeField]
    TextMeshProUGUI ResponseText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ARDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
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
