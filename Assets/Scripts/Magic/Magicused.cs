using UnityEngine;

public class Magicused : MonoBehaviour {

    // This script exist as a container of info that other scripts can get from

    public GameObject currentMagic; // The magic currently used 

    // Different sort of magics that can be used
    public GameObject pinkMagic;
    public GameObject purpleMagic;

    private Shoot shootscript; // The script that fires projectiles
    private GameObject fire; // The object that fires projectiles

	// Use this for initialization
	void Awake () {
		if (currentMagic == null)
        {
            Debug.LogError("no selected starting magic");
        }

        fire = GameObject.Find("Fire");
        shootscript = fire.GetComponent<Shoot>();
        
        if (shootscript == null)
        {
            Debug.LogError("no selected shoot");
        }

        shootscript.changeMagic(currentMagic);
    }
}
