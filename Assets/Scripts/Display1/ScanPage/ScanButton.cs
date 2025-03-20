using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using UnityEngine.UI;
using System.Collections.Generic;
public class ScanButton : MonoBehaviour, IPointerExitHandler, IPointerDownHandler, IPointerEnterHandler{
    public bool SecondHandIsKeepTouching;
    public float LerpValue { get { return _lerpValue; } }
    public bool CanScan { get { return _canScan; } }
    [Inject]
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private List<Sprite> _hands;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private List<Animator> _animators;
    [SerializeField] private ContDownAnimationHandler _contDownAnimationHandler;

    [Space(5)]
    bool _canScan = true;
    private float _lerpValueModifier = -1;

    public float _lerpValue=0;
    private float _previousLerpValue;
    public bool FirstHandIsKeepTouching{ get { return _firstHandIsKeepTouching; } }
    private bool _firstHandIsKeepTouching=false;
    private void Start()
    {
        foreach (var anim in _animators)
            anim.speed = _gameConfig.ScanAnimationSpeed;
    }
 
    private void OnEnable() {
        _lerpValueModifier = -1;
        _canScan = true;
        SecondHandIsKeepTouching = false;
        _firstHandIsKeepTouching = false;
    }
    private void OnDisable() {
        _lerpValueModifier = -1;
        _canScan = true;
        SecondHandIsKeepTouching = false;
        _firstHandIsKeepTouching = false;
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
        _firstHandIsKeepTouching = true;
        if (_canScan)
        {
            foreach (var anim in _animators)
                anim.SetBool("IsScanning", true);

            PlusTModifier();
          
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //_firstHandIsKeepTouching = false;
        //foreach (var anim in _animators)
        //    anim.SetBool("IsScanning", false);
        //MinusTModifier();
    }
    public void OnPointerEnter(PointerEventData eventData) {
        //_firstHandIsKeepTouching = true;
        //if (_canScan) {
        //    foreach (var anim in _animators)
        //        anim.SetBool("IsScanning", true);

        //    PlusTModifier();
        //}
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
        
        if (_lerpValue <= 1f && CanScan) {
            _lerpValue += _lerpValueModifier * Time.deltaTime * _gameConfig.ScanSpeed;
            
        }
        if (_lerpValue <= 0)
            _lerpValue = 0;
        
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
//#if UNITY_EDITOR
//        if (_canScan)
//        {
//            _lerpValue = 0.05f;
//            _lerpValueModifier = 1;
//        }
//#endif
//#if UNITY_STANDALONE

        if (CanScan && SecondHandIsKeepTouching)
        {
            if (_canScan)
            {
                if(_lerpValueModifier<0)
                    _contDownAnimationHandler.StartCountDown();
                //_lerpValue = 0.05f;
                _lerpValueModifier = 1;
            }
        }
//#endif

     }
    public void TryScan()
    {
     
        if (_canScan)
        {
            foreach (var anim in _animators)
                anim.SetBool("IsScanning", true);

            PlusTModifier();
            
        }
    }
    public void DonstScan()
    {
        foreach (var anim in _animators)
            anim.SetBool("IsScanning", false);
        MinusTModifier();
    }

  
}
