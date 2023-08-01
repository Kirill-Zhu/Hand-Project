using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
[CreateAssetMenu(menuName = "Custom/SRInstaller")]
public class SRInstaller : ScriptableObjectInstaller
{
    [SerializeField] GameConfig _gameConfig;

    public override void InstallBindings()
    {
        Container.BindInstance<GameConfig>(_gameConfig);
    }
}
