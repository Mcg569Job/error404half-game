using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private WeaponManager _weaponManager;
    private Healt _healt;

    private void Start()
    {
        _weaponManager = GetComponent<WeaponManager>();
        _healt = GetComponentInChildren<Healt>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            CollectItem(other.GetComponent<Item>());
        }
    }

    private void CollectItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.Bullet:
                _weaponManager.AddBullet(item.amount);
                break;
            case ItemType.RepairBox:
                _healt.GetDamage(-item.amount);
                break;
        }

        item.CollectMe();
    }

}
