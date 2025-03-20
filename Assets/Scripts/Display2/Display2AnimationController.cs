using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using Zenject;
[BurstCompile]
public class Display2AnimationController : MonoBehaviour {
    public float handFadeInSpeed = 1;
    public float handMoveSpeed = 1;
    public float timeToDestroyHand = 1;
    [Inject]
    [SerializeField] private GameConfig _gameConfig;
    [Inject]
    [SerializeField] private PageController _pageController;
    [Header("Scannning progress proerties")]
    [SerializeField] private GameObject _scanningProcesshandObj;
    [Header("Sucsecc Scanning properties")]
    [SerializeField] private GameObject _handPrefab;
    [SerializeField] private List<Sprite> _hands;
    
    [Header("Hand Transform")]
    [Space(10)]
    
    [SerializeField] private List<RectTransform> _blackHandsTransform;// index 0
    [SerializeField] private List<RectTransform> _redHandsTransform;//index 1
    [SerializeField] private List<RectTransform> _whiteHandsTransform;// index 2

    [Header("Hands Rotation")]
    [Space(10)]
    [SerializeField] private List<Vector3> _blackHandRotationList;
    [SerializeField] private List<Vector3> _redHandsRotationList;
    [SerializeField] private List<Vector3> _whiteHandsRotationList;


    private int _handIndex;
    private Vector3 _handStartPoint;
    private RectTransform _handFinishPoint;
    [SerializeField] private Vector3 _handStartScale;
    private Vector3 _handFinisScale = new Vector3(0.2f, 0.2f, 0.2f);
    private Vector3 _handStartLocalEulerAngles;
    private Vector3 _handFinishLocalEulerAngles;
    private CanvasGroup _handCanvasGroup;
    private Coroutine _coroutine;
    private GameObject _handObject;
    public float _tLerp = 0;
    private int[] _rightLeftHandRandom = new int[] { -1, 1 };
   

    private void Awake() {
       
    }
    private void OnEnable() {
        //StartValues
        _handStartPoint =GetComponent<RectTransform>().position;
        _handStartLocalEulerAngles = transform.localEulerAngles;
        //Scale
        _handStartScale = transform.GetComponent<RectTransform>().sizeDelta;
     

        _scanningProcesshandObj.GetComponent<RectTransform>().position = _handStartPoint;
        _scanningProcesshandObj.transform.localEulerAngles = _handStartLocalEulerAngles;
        
    }
    private void Update() {
        UpdateAlphaScanningProcessHandImage(_pageController.ScanProgress);

        _scanningProcesshandObj.GetComponent<RectTransform>().sizeDelta = _handStartScale;
    }

    public void SetScanningProperites(int handIndex) {

        _scanningProcesshandObj.GetComponent<Image>().sprite = _hands[handIndex];

        // sideModificator = _rightLeftHandRandom[Random.Range(0, _rightLeftHandRandom.Length)];
        //_sideModificator = 1;
       
        _scanningProcesshandObj.GetComponent<RectTransform>().sizeDelta = _handStartScale;

    }

    public void StartDisplay2Animation(int handIndex) {

        Debug.Log("Start Display 2 Animation");
        _handIndex = handIndex;
        _handObject = Instantiate(_handPrefab, transform);
        _handObject.GetComponent<RectTransform>().localPosition = _handStartPoint;
        _handObject.GetComponent<Image>().sprite = _hands[_handIndex];
        _handObject.GetComponent<Image>().SetNativeSize();
        var tmpLocalScale = _handStartScale;
   
        _handObject.GetComponent<RectTransform>().sizeDelta = tmpLocalScale;
        tmpLocalScale = _handObject.GetComponent<RectTransform>().localScale; 
     
        _handObject.GetComponent<RectTransform>().localScale = tmpLocalScale;
        _handCanvasGroup = _handObject.GetComponent<CanvasGroup>();
        _handCanvasGroup.alpha = 1;
       
        //Choose Where hand will fly
        int random = 0;
        switch (_handIndex) {
            case 0:
                random = Random.Range(0, _blackHandsTransform.Count - 1);
                _handFinishPoint = _blackHandsTransform[random];
                _handFinishLocalEulerAngles = _blackHandRotationList[random];
                _handFinisScale = _blackHandsTransform[random].sizeDelta;
                break;
            case 1:
                random = Random.Range(0, _redHandsTransform.Count - 1);
                _handFinishPoint = _redHandsTransform[random];
                _handFinishLocalEulerAngles = _redHandsRotationList[random];
                _handFinisScale = _redHandsTransform[random].sizeDelta;
                break;
            case 2:
                random = Random.Range(0, _whiteHandsTransform.Count - 1);
                _handFinishPoint = _whiteHandsTransform[random];
                _handFinishLocalEulerAngles = _whiteHandsRotationList[random];
                _handFinisScale = _whiteHandsTransform[random].sizeDelta;
                break;
        }

        _tLerp = 0;
        _coroutine = StartCoroutine(MoveHandToOtherHands());
    }
    private void UpdateAlphaScanningProcessHandImage(float alpha) {
        if (alpha > 0.15f) {
            _scanningProcesshandObj.GetComponent<CanvasGroup>().alpha = alpha;

        } else {
            _scanningProcesshandObj.GetComponent<CanvasGroup>().alpha = 0;
        }

    }

    private IEnumerator MoveHandToOtherHands() {
        _tLerp += Time.fixedDeltaTime * handMoveSpeed;
        _handObject.GetComponent<RectTransform>().position = Vector3.Lerp(_handStartPoint, _handFinishPoint.position, _tLerp);
        _handObject.GetComponent<RectTransform>().sizeDelta = Vector3.Lerp(_handStartScale, new Vector3(_handFinisScale.x , _handFinisScale.y, _handFinisScale.z), _tLerp);

        _handObject.transform.localEulerAngles = Vector3.Lerp(_handStartLocalEulerAngles,_handFinishLocalEulerAngles, _tLerp*1.1f);
        yield return new WaitForFixedUpdate();
        if (_tLerp < 1) {
            _coroutine = StartCoroutine(MoveHandToOtherHands());
            Debug.Log("move hand");
        } else {
            StopCoroutine(_coroutine);
            StartCoroutine(StartTimerToDestroyHand(timeToDestroyHand));
        }

    }
    private IEnumerator StartTimerToDestroyHand(float sec) {
        GameObject tmpObj = _handObject;
        yield return new WaitForSeconds(sec);
        Destroy(tmpObj);
    }
}
