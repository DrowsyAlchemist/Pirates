using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 51)]
public class WeaponInfo : ScriptableObject
{
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
}
