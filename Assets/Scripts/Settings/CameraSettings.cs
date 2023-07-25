using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Settings/Camera")]
public class CameraSettings : ScriptableObject
{
    [SerializeField, Range(-90, 90)] private float _xMinRotation;
    [SerializeField, Range(-90, 90)] private float _xMaxRotation;
    [SerializeField, Range(-90, 90)] private float _yMinRotation;
    [SerializeField, Range(-90, 90)] private float _yMaxRotation;

    public float XMinRotation => _xMinRotation;
    public float XMaxRotation => _xMaxRotation;
    public float YMinRotation => _yMinRotation;
    public float YMaxRotation => _yMaxRotation;
}