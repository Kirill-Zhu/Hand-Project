using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Zenject;
public class VideoPlayerController : MonoBehaviour
{
    [Inject]
    [SerializeField] private ScanButton _scanButton;
    public VideoPlayer _player;
    public long currentFrame;
    public float x;
    public ulong frames;
    public float t;
    private void Start()
    {
        _player = GetComponent<VideoPlayer>();
        frames = _player.frameCount;


    }
    private void Update()
    {




    }
    public void Reached(VideoPlayer _player)
    {
        Debug.Log("Loop Point Reached");
    }
}
