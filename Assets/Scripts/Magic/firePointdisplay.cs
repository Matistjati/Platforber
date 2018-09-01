using UnityEngine;

public class firePointdisplay : MonoBehaviour
{

    [SerializeField] private int rotationoffset = 90; // If the rotation doesn't follow the mouse properly, test changing this value
    [SerializeField] private Sprite emptySprite; // A completely empty sprite

    private float reGrowthTime; // The period when the firepoint will regrow
    private SpriteRenderer spriteRenderer;
    private float originalScaleX; // The original object scale, used to set back X
    private float originalScaleY; // Same as upper, but with Y

    private float reloadTime; // Used to calculate regrowth
    private float reGrowthAmunt; // How much we will need to increment the growth by, multiplied by time.deltatime

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (emptySprite == null)
        {
            Debug.Log("no empty sprite assigned");
        }

        originalScaleX = this.transform.localScale.x;
        originalScaleY = this.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < reGrowthTime)
        {
            transform.localScale += new Vector3(reGrowthAmunt * Time.deltaTime, reGrowthAmunt * Time.deltaTime, 0);
            if (originalScaleX - transform.localScale.x < 0.005f) // Setting the size to its exact original value when coming close
            {
                transform.localScale = new Vector3(originalScaleX, originalScaleY, 0);
            }
        }

        // Find the angle between the firepoint and cursor
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationoffset); // Rotating the firepoint to aim at the cursor

    }

    // The firepoint will become really small and then grow
    public void InitiateSpriteGrowth(float waitTime)
    {
        transform.localScale = new Vector3(0.01f, 0.01f, transform.localScale.z);
        reloadTime = 1 / waitTime;
        reGrowthAmunt = waitTime / reloadTime;
        reGrowthTime = Time.time + 1 / waitTime;
    }

    // Method for securely updating the sprite displayed
    public void updateSprite(Sprite newSprite)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = newSprite;
    }
}
