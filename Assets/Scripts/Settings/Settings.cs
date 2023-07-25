using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _coroutineObject;
    [SerializeField] private ShootingSettings _shootingSettings;
    [SerializeField] private CameraSettings _cameraSettings;
    [SerializeField] private LeaderboardSettings _leaderboardSettings;

    private static Settings _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static MonoBehaviour CoroutineObject => _instance._coroutineObject;
    public static ShootingSettings ShootingSettings => _instance._shootingSettings;
    public static CameraSettings CameraSettings => _instance._cameraSettings;
    public static LeaderboardSettings LeaderboardSettings => _instance._leaderboardSettings;
}
