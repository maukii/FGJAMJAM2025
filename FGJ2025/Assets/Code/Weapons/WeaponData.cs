using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon System/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public Projectile projectilePrefab;
    public float fireRate = 0.5f;
    public float projectileSpeed = 20f;
    public float damage = 10f;
}
