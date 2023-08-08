using System;
using TMPro;
using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    [SerializeField] private UIButton _button;
    [SerializeField] private TMP_Text _number;

    [SerializeField] private RectTransform[] _stars;

    private LevelPreset _level;

    public event Action<LevelPreset> Clicked;

    private void Awake()
    {
        _button.SetOnClickAction(OnButtonClick);
    }

    public void Render(LevelPreset level)
    {
        _level = level ?? throw new ArgumentNullException();
        _number.text = (level.IndexInLocation + 1).ToString();

        int i = 0;

        for (; i < level.Stars; i++)
            _stars[i].Activate();

        for (; i < _stars.Length; i++)
            _stars[i].Deactivate();
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(_level);
    }
}