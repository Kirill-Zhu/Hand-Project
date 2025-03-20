using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] PageController _pageController;
    [SerializeField] ScanButton _scanbutton;
    [SerializeField] VideoPlayerController _videoPlayerController;
  
    public override void InstallBindings()
    {
        Container.BindInstance<PageController>(_pageController);
        Container.BindInstance<ScanButton>(_scanbutton);
        Container.BindInstance<VideoPlayerController>(_videoPlayerController);
     
    }
}
