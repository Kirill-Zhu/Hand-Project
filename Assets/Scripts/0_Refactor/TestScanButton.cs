using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestScanButton : MonoBehaviour
{
    Button _button;

    private void Awake() {
       
    }

    public void Test() {
        Debug.Log("Hand Detected");
    }
    public void Test(PointerEventData data) {
        Debug.Log("Hand Detected");
    }
}
