using UnityEngine;

[CreateAssetMenu(fileName = "new character stats", menuName = "character stats")]
public class CharacterStats : ScriptableObject
{
    public float health;       // The maximum health
    public float healthRegen;  // The rate health replenishes at

    public float mana;         // The maximum mana
    public float manaRegen;    // The rate mana replenishes at
}
