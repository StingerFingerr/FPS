using UnityEngine;

[CreateAssetMenu(menuName = "Player/Create poisoning settings", fileName = "Poisoning settings")]
public class PoisoningSettings: ScriptableObject
{
    public float poisoningDelay = .5f;
    public int poisoningDamage = 5;
}