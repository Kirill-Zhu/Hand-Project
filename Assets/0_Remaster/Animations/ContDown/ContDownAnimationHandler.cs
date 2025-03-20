using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ContDownAnimationHandler : MonoBehaviour
{
    [SerializeField] private int timeToCount = 5;
    private TextMeshProUGUI _textMesh;
    private IEnumerator _couroutine;
    private void Awake() {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.text = null;
    }
    private void OnEnable() {
        _textMesh.text = null;
    }
    [ContextMenu("StartCountDown")]
    public void StartCountDown() {
        StopAllCoroutines();
        _textMesh.text = null;
        if(_couroutine != null)
            StopCoroutine(_couroutine);

        _couroutine = StarCountDown();
        StartCoroutine(_couroutine);
    }
    public void SetText(int number) {
        _textMesh.text = null;
        _textMesh.text = number.ToString();
    }

    private IEnumerator StarCountDown() {
        Debug.Log("Strart CountDown Coroutine");
        SetText(5);
        for (int i = timeToCount; i > -1; i--) { 
            SetText(i);
            yield return new WaitForSeconds(1f);
        }
    }
}
