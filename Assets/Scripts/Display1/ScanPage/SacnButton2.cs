using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SacnButton2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Inject]
    [SerializeField] private ScanButton _scanButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _scanButton.SecondHandIsKeepTouching = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _scanButton.SecondHandIsKeepTouching = false;
    }
}
