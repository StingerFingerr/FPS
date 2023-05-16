using UnityEngine;
using Zenject;

public class ArmoredEnemyBodyPart: EnemyBodyPart
{
    [SerializeField] private EnemyArmorType armorType;
    [SerializeField] [Range(10, 100)] private int armorChance = 50;
    [SerializeField] private Vector3 armorPosition;

    private float _damageMultiplier = 1f;

    [Inject]
    private void Construct(EnemyArmorsProvider armorsProvider)
    {
        if (armorChance == 100 ||
            Random.Range(0, 100) <= armorChance)
        {
            var data = armorsProvider.GetData(armorType);

            var armor = Instantiate(data.armorPrefab).transform;
            armor.SetParent(transform);
            armor.localPosition = armorPosition;
            armor.localEulerAngles = Vector3.zero;

            _damageMultiplier = data.damageMultiplier;
        }
    }

    public override void SetDamage(float damage, Vector3 hitNormal)
    {
        base.SetDamage(damage * _damageMultiplier, hitNormal);
    }
}