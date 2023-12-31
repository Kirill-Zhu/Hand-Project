using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using UnityEngine.UI;
using System.Collections.Generic;
public class ScanButton : MonoBehaviour, IPointerExitHandler, IPointerDownHandler
{
    public bool SecondHandIsKeepTouching;
    public float LerpValue { get { return _lerpValue; } }
    public bool CanScan { get { return _canScan; } }
    [Inject]
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private List<Sprite> _hands;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private List<Animator> _animators;

    bool _canScan = true;
    private float _lerpValueModifier = -1;
    public float _lerpValue;
    private float _previousLerpValue;
    private void Start()
    {
        foreach (var anim in _animators)
            anim.speed = _gameConfig.ScanAnimationSpeed;
    }
    private void Update()
    {

        UpdateLerpValue();
        UpdateHandsAplha();

    }
    public void SetHandsColor(int handIndex)
    {
        _leftHand.GetComponent<Image>().sprite = _hands[handIndex];
        _rightHand.GetComponent<Image>().sprite = _hands[handIndex];
        _leftHand.GetComponent<CanvasGroup>().alpha = 0;
        _rightHand.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_canScan)
        {
            foreach (var anim in _animators)
                anim.SetBool("IsScanning", true);

            PlusTModifier();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var anim in _animators)
            anim.SetBool("IsScanning", false);
        MinusTModifier();


    }
    public void SetCanScanTrue()
    {
        _canScan = true;
    }
    public void StartDisplay2Animation()
    {
        _lerpValue = 0;
        _canScan = false;
        foreach (var anim in _animators)
            anim.SetBool("IsScanning", false);

        Debug.Log("Scan button start Animation 2");

    }
    private void UpdateLerpValue()
    {
        if (_lerpValue >= 0.05f && _lerpValue <= 1f && CanScan)
            _lerpValue += _lerpValueModifier * Time.deltaTime * _gameConfig.ScanSpeed;
    }
    private void UpdateHandsAplha()
    {
        if (_previousLerpValue != _lerpValue && _canScan)
        {
            _leftHand.GetComponent<CanvasGroup>().alpha = _lerpValue;
            _rightHand.GetComponent<CanvasGroup>().alpha = _lerpValue;
        }

        _previousLerpValue = _lerpValue;
    }

    private void MinusTModifier()
    {

        _lerpValueModifier = -1;
    }
    private void PlusTModifier()
    {
#if UNITY_EDITOR
        if (_canScan)
        {
            _lerpValue = 0.05f;
            _lerpValueModifier = 1;
        }
#endif
#if UNITY_STANDALONE

        if (CanScan && SecondHandIsKeepTouching)
        {
            if (_canScan)
            {
                _lerpValue = 0.05f;
                _lerpValueModifier = 1;
            }
        }
#endif

    }


}
