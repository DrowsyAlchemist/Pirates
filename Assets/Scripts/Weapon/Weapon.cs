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

    private HitEffect _hitEffect;
    private Timer _timer;

    public string Id => _id;
    public Sprite Sprite => _sprite;
    public AudioClip ShootClip => _shootClip;
    public int Cost => _cost;
    public int Damage => _damage;
    public float SecondsBetweenShots => _secondsBetweenShots;
    public bool IsReady { get; private set; } = true;
    public float SecondsBeforeReadyLeft => SecondsBetweenShots - _timer.ElapsedTime;

    public event Action Shooted;
    public event Action ReloadingFinished;

    public void Init(HitEffect hitEffect)
    {
        _hitEffect = hitEffect;
        _shotEffect.Play();
        _timer = new Timer();
        _timer.WentOff += OnTimerWentOff;
    }

    private void OnDestroy()
    {
        _timer.WentOff -= OnTimerWentOff;
    }

    public void Shoot(RaycastHit hit, int playerDamage)
    {
        if (IsReady == false)
            throw new InvalidOperationException("Weapon is not ready");

        if (_animator.IsPlaying)
            throw new InvalidOperationException("Animator is still playing");

        IsReady = false;
        _shotEffect.Play();
        _animator.PlayShot();
        _timer.Start(SecondsBetweenShots);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out IApplyDamage target))
            {
                target.ApplyDamage(Damage + playerDamage);
            }
            else
            {
                _hitEffect.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
                _hitEffect.Play();
            }
        }
        Shooted?.Invoke();
    }

    private void OnTimerWentOff()
    {
        IsReady = true;
        ReloadingFinished?.Invoke();
    }
}
