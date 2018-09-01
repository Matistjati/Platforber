using UnityEngine;

public class MagicManager : MonoBehaviour {

    public GameObject thisButtonMagic; // The magic that will be changed to when pressing this button
    public GameObject fire; // The gameobject that fires projectiles

    // Checking that everything is assigned
    void Start () {
        if (thisButtonMagic == null)
        {
            Debug.Log("no currentmagic assigned");
        }

        if (fire == null)
        {
            Debug.Log("no fire assigned");
        }
	}

    // Changing the players magic when clicking this button
    public void changeMagicClicked()
    {
        Shoot magicHandler = Shoot.Instance.GetComponent<Shoot>();
        magicHandler.changeMagic(thisButtonMagic);
        transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        fire.GetComponent<Shoot>().mouseExitButton();
    }
}
