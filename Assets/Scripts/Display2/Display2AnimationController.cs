using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class Display2AnimationController : MonoBehaviour
{
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
    [SerializeField] private List<Transform> _blackHandsTransform;// index 0
    [SerializeField] private List<Transform> _redHandsTransform;//index 1
    [SerializeField] private List<Transform> _whiteHandsTransform;// index 2
    private int _handIndex;
    private Vector3 _handStartPoint;
    private Transform _handFinishPoint;
    private Vector3 _handStartScale;
    private Vector3 _handeFinisScale = new Vector3(0.2f, 0.2f, 0.2f);
    private CanvasGroup _handCanvasGroup;
    private Coroutine _coroutine;
    private GameObject _handObject;
    public float _tLerp = 0;
    private int[] _rightLeftHandRandom = new int[] { -1, 1 };
    private int sideModificator;

    private void Update()
    {
        UpdateAlphaScanningProcessHandImage(_pageController.ScanProgress);
    }

    public void SetScanningProperites(int handIndex)
    {

        _scanningProcesshandObj.GetComponent<Image>().sprite = _hands[handIndex];

        // sideModificator = _rightLeftHandRandom[Random.Range(0, _rightLeftHandRandom.Length)];
        sideModificator = 1;
        var tmpScale = _scanningProcesshandObj.transform.localScale;
        tmpScale.x = sideModificator;
        _scanningProcesshandObj.transform.localScale = tmpScale;

    }

    public void StartDisplay2Animation(int handIndex)
    {
        _handIndex = handIndex;
        _handObject = Instantiate(_handPrefab, transform);
        _handObject.GetComponent<Image>().sprite = _hands[_handIndex];
        _handObject.GetComponent<Image>().SetNativeSize();
        var tmpLocalScale = _handObject.transform.localScale;
        tmpLocalScale.x = sideModificator;
        _handObject.transform.localScale = tmpLocalScale;
        _handCanvasGroup = _handObject.GetComponent<CanvasGroup>();
        _handCanvasGroup.alpha = 1;

        _handStartScale = _handObject.transform.localScale;
        _handStartPoint = _handObject.transform.position;
        //Choose Where hand will fly

        switch (_handIndex)
        {
            case 0:
                _handFinishPoint = _blackHandsTransform[Random.Range(0, _blackHandsTransform.Count - 1)];
                break;
            case 1:
                _handFinishPoint = _redHandsTransform[Random.Range(0, _redHandsTransform.Count - 1)];
                break;
            case 2:
                _handFinishPoint = _whiteHandsTransform[Random.Range(0, _whiteHandsTransform.Count - 1)];
                break;
        }
        _tLerp = 0;
        _coroutine = StartCoroutine(MoveHandToOtherHands());
    }
    private void UpdateAlphaScanningProcessHandImage(float alpha)
    {
        if (alpha > 0.15f)
        {
            _scanningProcesshandObj.GetComponent<CanvasGroup>().alpha = alpha;

        }
        else
        {
            _scanningProcesshandObj.GetComponent<CanvasGroup>().alpha = 0;
        }

    }

    private IEnumerator MoveHandToOtherHands()
    {
        _tLerp += Time.fixedDeltaTime * handMoveSpeed;
        _handObject.transform.position = Vector3.Lerp(_handStartPoint, _handFinishPoint.position, _tLerp);
        _handObject.transform.localScale = Vector3.Lerp(_handStartScale,

        new Vector3(_handeFinisScale.x * sideModificator, _handeFinisScale.y, _handeFinisScale.z), _tLerp);
        yield return new WaitForFixedUpdate();
        if (_tLerp < 1)
        {
            _coroutine = StartCoroutine(MoveHandToOtherHands());
            Debug.Log("move hand");
        }

        else
        {
            StopCoroutine(_coroutine);
            StartCoroutine(StartTimerToDestroyHand(timeToDestroyHand));
        }

    }
    private IEnumerator StartTimerToDestroyHand(float sec)
    {
        GameObject tmpObj = _handObject;
        yield return new WaitForSeconds(sec);
        Destroy(tmpObj);
    }
}
