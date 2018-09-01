using UnityEngine;

public class MagicusedUI : MonoBehaviour {

    public GameObject magicSelector; // The ui displayed for changing magic

	void Start () {
        if (magicSelector == null)
        {
            Debug.Log("no magicselector assigned");
        }
	}
	
	void Update () {
        // The ui to foreground or background when rightclicking
        if (Input.GetMouseButtonDown(1))
        {
            magicSelector.transform.parent.gameObject.SetActive(!magicSelector.transform.parent.gameObject.activeSelf);
            magicSelector.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}
