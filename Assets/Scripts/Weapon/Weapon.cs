using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponAnimator _animator;
    [SerializeField] private ParticleSystem _shotEffect;

    [SerializeField] private string _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private int _cost;

    [SerializeField] private int _damage;
    [SerializeField] private float _secondsBetweenShots;

    public string Id => _id;
    public Sprite Sprite => _sprite;
    public AudioClip ShootClip => _shootClip;
    public int Cost => _cost;
    public int Damage => _damage;
    public float SecondsBetweenShots => _secondsBetweenShots;

    private Timer _timer;

    public bool IsReady => SecondsBeforeReadyLeft < Settings.Epsilon;
    public float SecondsBeforeReadyLeft => SecondsBetweenShots - _timer.ElapsedTime;

    public event Action Shooted;

    public void Init()
    {
        _shotEffect.Play();
        _timer = new Timer();
        _timer.Start(SecondsBetweenShots);
    }

    public void Shoot(RaycastHit hit, int playerDamage)
    {
        if (IsReady == false)
            throw new InvalidOperationException();

        if (_animator.IsPlaying)
            throw new InvalidOperationException("Animator is still playing");

        _shotEffect.Play();
        _animator.PlayShot();
        _timer.Start(SecondsBetweenShots);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent(out IApplyDamage target))
                target.ApplyDamage(Damage + playerDamage);

        Shooted?.Invoke();
    }
}
