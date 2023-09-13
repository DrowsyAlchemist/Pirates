using Agava.WebUtility;
using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private bool _useMobileControlInEditor;
    [SerializeField] private InputController _inputController;
    [SerializeField] private Sensitivity _sensitivity;
    [SerializeField] private ShootingPoint _shootingPoint;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Level _level;

    [SerializeField] private Canvas _backgroundCanvas;

    private Player _player;
    private Saver _saver;

    private IEnumerator Start()
    {
#if UNITY_EDITOR
        yield return Init();
        yield break;
#endif
        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();

        yield return Init();
    }

    private IEnumerator Init()
    {
#if !UNITY_EDITOR
        string currentLang = YandexGamesSdk.Environment.GetCurrentLang();
        LeanLocalization.SetCurrentLanguageAll(currentLang);
#endif
        _saver = new Saver();

        while (_saver.IsReady == false)
            yield return null;

        InitInputController();
        _player = new Player(_inputController, _saver);
        _level.Init(_player, _saver);
        _mainMenu.Init(_player, _saver);
        _mainMenu.Open();
        _shootingPoint.Init(_player, _inputController);
        _backgroundCanvas.Deactivate();
    }

    public void RemoveSaves()
    {
        _saver.RemoveSaves();
    }

    public void UnlockAllLevels()
    {
        _saver.UnlockAllLevels();
    }

    private void InitInputController()
    {
        bool isMobile;
        _sensitivity.Init(_saver);
#if UNITY_EDITOR
        isMobile = _useMobileControlInEditor;
        _inputController.Init(isMobile, _sensitivity);
        return;
#endif
        isMobile = Device.IsMobile;
        _inputController.Init(isMobile, _sensitivity);
    }
}
