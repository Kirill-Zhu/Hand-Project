using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SacnButton2 : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler, IPointerDownHandler
{
    [Inject]
    [SerializeField] private ScanButton _scanButton;

    private void Start() {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }
    public void OnPointerDown(PointerEventData eventData) {
        _scanButton.SecondHandIsKeepTouching = true;
        if (_scanButton.FirstHandIsKeepTouching)
            _scanButton.TryScan();
        Debug.Log("Scan button is keep touchin is " + _scanButton.SecondHandIsKeepTouching);
    }
    private void OnDisable() {
        _scanButton.SecondHandIsKeepTouching = false;
    }
    private void OnEnable() {
        _scanButton.SecondHandIsKeepTouching = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
        //_scanButton.SecondHandIsKeepTouching = false;
        //_scanButton.DonstScan();
        //Debug.Log("Scan button is keep touchin is " + _scanButton.SecondHandIsKeepTouching);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //_scanButton.DonstScan();
        //_scanButton.SecondHandIsKeepTouching = false;
        //Debug.Log("Scan button is keep touchin is " + _scanButton.SecondHandIsKeepTouching);
    }

   
}
