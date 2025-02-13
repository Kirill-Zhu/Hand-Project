using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class HandsHDRController : MonoBehaviour
{
    [Inject]
    [SerializeField] private GameConfig _gameConfig;
    [Range(0, 1)]
    [SerializeField] float _handsStartAlpha;
    [Range(2, 10)]
    [SerializeField] private float _minSecInterval = 5;
    [Range(3, 12)]
    [SerializeField] private float _maxSecInterval = 7;
    [Range(0.35f, 5)]
    [SerializeField] private float _animationSpeed = 0.5f;
    [SerializeField] private List<Image> _hands;
    private Image _currentHand;
    private float _timerToNextAnimation;
    private float _currentHandAlpha;
    private Coroutine _coroutine;

    private void Start()
    {
        _handsStartAlpha = _gameConfig.HandStartAlpha;
        _minSecInterval = _gameConfig.MinSecIntervalBetwenShine;
        _maxSecInterval = _gameConfig.MaxSecIntervalBetwenShine;
        _animationSpeed = _gameConfig.AlphaChangeSpeed;

        foreach (var hand in _hands)
            hand.color = new Color(hand.color.r, hand.color.g, hand.color.b, _handsStartAlpha);

        SetRandomTimeToNextAnimation();
    }

    private void Update()
    {
        UpdateTimerIntervalBetweenAnimations();
    }

    private void UpdateTimerIntervalBetweenAnimations()
    {
        _timerToNextAnimation -= Time.deltaTime;
        if (_timerToNextAnimation <= 0)
        {
            SetRandomTimeToNextAnimation();
            StartHDRRandomHandImage();
        }
    }
    private void SetRandomTimeToNextAnimation()
    {
        _timerToNextAnimation = Random.Range(_minSecInterval, _maxSecInterval);
    }
    [ContextMenu("Start RandomHandHDRAnimation")]
    private void StartHDRRandomHandImage()
    {
        int randomIndex = Random.Range(0, _hands.Count - 1);
        _currentHand = _hands[randomIndex];
        _currentHandAlpha = _handsStartAlpha + Time.fixedDeltaTime;
        _coroutine = StartCoroutine(IncreaseCurrentHandAlpha());
    }
    private IEnumerator IncreaseCurrentHandAlpha()
    {
        _currentHandAlpha += Time.fixedDeltaTime * _animationSpeed;
        _currentHand.color = new Color(_currentHand.color.r, _currentHand.color.g, _currentHand.color.b, _currentHandAlpha);
        yield return new WaitForFixedUpdate();
        if (_currentHandAlpha < 1)
            StartCoroutine(IncreaseCurrentHandAlpha());
        else
        {
            _currentHandAlpha = 1;
            _currentHand.color = new Color(_currentHand.color.r, _currentHand.color.g, _currentHand.color.b, _currentHandAlpha);

            _coroutine = StartCoroutine(DecreaseCurrentHandAlpha());
        }

    }
    private IEnumerator DecreaseCurrentHandAlpha()
    {
        _currentHandAlpha -= Time.fixedDeltaTime * _animationSpeed;
        _currentHand.color = new Color(_currentHand.color.r, _currentHand.color.g, _currentHand.color.b, _currentHandAlpha);

        yield return new WaitForFixedUpdate();
        if (_currentHandAlpha > _handsStartAlpha)
            StartCoroutine(DecreaseCurrentHandAlpha());
        else
        {
            _currentHandAlpha = _handsStartAlpha;
            _currentHand.color = new Color(_currentHand.color.r, _currentHand.color.g, _currentHand.color.b, _currentHandAlpha);
           // _currentHand.transform.localScale = new Vector3(1, 1, 1);
            StopCoroutine(_coroutine);
            _coroutine = null;

        }


    }
}
