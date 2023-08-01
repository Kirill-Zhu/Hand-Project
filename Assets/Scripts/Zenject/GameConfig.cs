using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GameConfigs")]
public class GameConfig : ScriptableObject
{
    [Header("Display 1 Configs")]
    public float ScanSpeed = 0.3f;
    public float ScanAnimationSpeed = 1f;

    [Header("Display 2 Configs")]
    public float HandFadeInSpeed = 1;
    public float HandMoveSpeed = .3f;
    [Tooltip("Через сколько рука уничтожится после того как она достигнет " +
        "другой руки на стене")]
    public float DestroyHandSec = 1;
    [Header("Diplay 2 BackGround Annimation Configs")]
    [Range(0, 1)]
    public float HandStartAlpha = 0.65f;
    [Range(2, 12)]
    public float MinSecIntervalBetwenShine = 2f;
    [Range(3, 13)]
    public float MaxSecIntervalBetwenShine = 3f;
    [Range(.1f, 5)]
    public float AlphaChangeSpeed = 0.5f;
}
