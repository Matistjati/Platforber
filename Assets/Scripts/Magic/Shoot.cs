using UnityEngine;

public class Shoot : MonoBehaviour
{
    public static Shoot Instance;    // Singleton

    // these values are changed from other scripts
    public float tappingFireRate;    // Tapping firerate
    public float holdingFireRate;    // Holding down fire rate
    private int manaCost;            // The cost in mana for firing the magic

    private float timeToFire;        // Used as a timer for when not to fire
    private SpriteRenderer firePointSprite;
    private firePointdisplay firePointSpriteHandler;
    private bool mouseHoveringOverButton; // We won't fire if hovering over a ui button

    public GameObject firePoint;     // Gameobject whose rotation and position we will use when instantiating projectiles
    public GameObject projectile;    // The project that we will shoot


    // Use this for initialization
    void Start()
    {
        // Checking that everything needed is assigned
        Instance = this;

        if (firePoint == null)
        {
            Debug.LogError("firePoint not assigned");
        }
        if (projectile == null)
        {
            Debug.LogError("projectile not assigned");
        }
        firePointSprite = firePoint.GetComponent<SpriteRenderer>();
        if (firePointSprite == null)
        {
            Debug.LogError("firepoint sprite renderer not found");
        }
        firePointSpriteHandler = firePoint.GetComponent<firePointdisplay>();
        if (firePointSpriteHandler == null)
        {
            Debug.LogError("firepoint sprite handler renderer not found");
        }
    }

    public void changeMagic(GameObject magic)
    {
        // Method for other scripts to change the magic currently used
        projectileHandler magicProperties = magic.GetComponent<projectileHandler>();
        SpriteRenderer magicSprite = magic.GetComponent<SpriteRenderer>();

        if (firePointSpriteHandler == null)
        {
            firePointSpriteHandler = firePoint.GetComponent<firePointdisplay>();
            if (firePointSpriteHandler == null)
            {
                Debug.LogError("firepoint sprite handler renderer not found");
            }
        }
        firePointSpriteHandler.updateSprite(magicSprite.sprite);
        tappingFireRate = magicProperties.projectileInfo.tappingFireRate;
        holdingFireRate = magicProperties.projectileInfo.holdingFireRate;
        projectile = magic;
        manaCost = magicProperties.projectileInfo.manaCost;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<HpMp>().HasEnoughMana(manaCost))
        {
            if (holdingFireRate == 0)
            {
                if (Input.GetButtonDown("Fire1") && Time.time > timeToFire && !mouseHoveringOverButton)
                {
                    timeToFire = Time.time + 1 / tappingFireRate;
                    firePointSpriteHandler.InitiateSpriteGrowth(tappingFireRate);
                    shoot();
                }
            }
            else
            {
                if (Input.GetButton("Fire1") && Time.time > timeToFire && !mouseHoveringOverButton)
                {
                    timeToFire = Time.time + 1 / holdingFireRate;
                    firePointSpriteHandler.InitiateSpriteGrowth(holdingFireRate);
                    shoot();
                }
            }
        }
    }
    

    void shoot()
    {
        // Getting the position of the firepoint
        Vector2 firePointPosition = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);

        // Instantiating the projectile
        Instantiate(projectile, firePointPosition, firePoint.transform.rotation);
        GameObject.Find("Player").GetComponent<HpMp>().SpendMana(manaCost);
    }

    // We will not be able to fire, since we are hovering over a button
    public void mouseEnterButton()
    {
        mouseHoveringOverButton = true;
    }

    // We will now be able to fire, as we are no longer hobering over a button
    public void mouseExitButton()
    {
        mouseHoveringOverButton = false;
    }
}
