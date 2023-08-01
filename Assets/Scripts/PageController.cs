using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Threading.Tasks;
public class PageController : MonoBehaviour
{
    public float ScanProgress;
    [Tooltip(" If scanning was successed returns to Choose Color menu After Value")]
    [SerializeField] private int _restartAfter = 3;
    public int ChossedColorIndex { get { return _choosedColor; } }
    [Header("Display 1 Properies")]
    [SerializeField] private GameObject _chooseColorPage;
    [SerializeField] private GameObject _scanHandPage;
    [SerializeField] private List<Sprite> _scanCircles;
    [SerializeField] private Image _scanRightHandImage;
    [SerializeField] private Image _scanLeftHandImage;
    [Inject]
    private ScanButton _scanButton;
    [Header("Display 2 Properies")]
    //[SerializeField] private Animator _Display2Animation;//пока условно
    [Inject]
    [SerializeField] private Display2AnimationController _Display2AnimController;
    private int _choosedColor;

    private void Update()
    {
        ScanProgress = _scanButton.LerpValue;
        if (ScanProgress >= 1f && _scanButton.CanScan)
        {
            _scanButton.StartDisplay2Animation();
            StartDisplay2Animation();
        }
    }
    public void ChooseColor(int index)
    {
        if (index >= 0 && index < _scanCircles.Count)
        {
            _choosedColor = index;
            _scanRightHandImage.sprite = _scanCircles[_choosedColor];
            _scanLeftHandImage.sprite = _scanRightHandImage.sprite;
            _Display2AnimController.SetScanningProperites(_choosedColor);
            _scanButton.SetHandsColor(index);
        }

        _scanRightHandImage.SetNativeSize();
        _scanLeftHandImage.SetNativeSize();
    }
    public void OpenScanPage()
    {
        _chooseColorPage.SetActive(false);
        _scanHandPage.SetActive(true);
    }
    public void OpenChoseColorPage()
    {
        _chooseColorPage.SetActive(true);
        _scanHandPage.SetActive(false);

    }
    [ContextMenu("Start Display 2 Animation")]
    public async void StartDisplay2Animation()
    {
        _Display2AnimController.StartDisplay2Animation(_choosedColor);
        await Task.Delay(_restartAfter * 1000);
        _scanButton.SetCanScanTrue();
        OpenChoseColorPage();
        Debug.Log("Scan button can scan = " + _scanButton.CanScan);
    }
}
