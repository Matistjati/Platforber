using UnityEngine;

[CreateAssetMenu(fileName = "new projectile info", menuName = "projectile info")]
public class ProjectileInfo : ScriptableObject
{
    public int velocity;
    public float holdingFireRate;
    public float tappingFireRate;

    public int manaCost;
    public int damage;

    public LayerMask whatToHit;
    public Transform destroyedParticles;
}
