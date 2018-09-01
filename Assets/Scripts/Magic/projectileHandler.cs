using UnityEngine;

public class projectileHandler : MonoBehaviour
{
    public ProjectileInfo projectileInfo;
    private float holdingFireRate; // Holding down
    private float tappingFireRate; // Single press
    private int velocity;
    private int damage;
    private int manaCost;

    private LayerMask whatToHit;
    private Transform destroyedParticles;

    private void Start()
    {
        holdingFireRate = projectileInfo.holdingFireRate;
        tappingFireRate = projectileInfo.tappingFireRate;
        velocity = projectileInfo.velocity;
        damage = projectileInfo.damage;
        manaCost = projectileInfo.manaCost;
        whatToHit = projectileInfo.whatToHit;
        destroyedParticles = projectileInfo.destroyedParticles;

        if (destroyedParticles == null)
        {
            Debug.LogError("No destroyedparticles assigned");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * velocity);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatToHit) != 0) // Checking if we want to hit the object
        {
            
            HpMp colliderHealth = collision.GetComponent<HpMp>(); // Getting the collided object's health script

            // Sometimes we want to destroy the projectile but the object doesn't have health
            try
            {
                colliderHealth.TakeDamage(damage); // Dealing the damage
            }
            catch (System.NullReferenceException)
            {

            }
            
            Destroy(this.gameObject);

            Vector2 destructionPosition = new Vector2(this.transform.position.x, this.transform.position.y);

            if (destroyedParticles != null)
            {
                var particle = Instantiate(destroyedParticles, destructionPosition, this.transform.rotation);

                ParticleSystem particleSystem = destroyedParticles.GetComponent<ParticleSystem>();

                Destroy(particle.gameObject, particleSystem.main.duration);
            }
        }
    }
}
