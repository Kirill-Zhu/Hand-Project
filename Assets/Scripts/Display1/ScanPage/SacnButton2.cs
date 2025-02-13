using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SacnButton2 : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    [Inject]
    [SerializeField] private ScanButton _scanButton;
  
    public void OnPointerEnter(PointerEventData eventData)
    {
        _scanButton.SecondHandIsKeepTouching = true;
        if (_scanButton.FirstHandIsKeepTouching)
            _scanButton.TryScan();
        Debug.Log("Scan button is keep touchin is " + _scanButton.SecondHandIsKeepTouching);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        _scanButton.SecondHandIsKeepTouching = false;
        _scanButton.DonstScan();
        Debug.Log("Scan button is keep touchin is " + _scanButton.SecondHandIsKeepTouching);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _scanButton.DonstScan();
        _scanButton.SecondHandIsKeepTouching = false;
        Debug.Log("Scan button is keep touchin is " + _scanButton.SecondHandIsKeepTouching);
    }
}
