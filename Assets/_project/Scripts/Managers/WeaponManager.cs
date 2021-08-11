using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Transform _spawnPoint;

    [Header("Bullet")]
    [SerializeField] private GameObject _bulletPrefab;


    [Header("Weapon Properties")]
    [SerializeField] private float attackTime;
    [SerializeField] private int damage;
    [SerializeField] private int magazine;
    private float _currentTime;
    private bool _available;
    private void Start()
    {
        AddBullet(100);
        _currentTime = attackTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime = attackTime;
            }
        }

    }


    private void Fire()
    {

        if(ErrorManager.Instance.ErrorType == ErrorType.DefectiveWeapons)
        {
            UIManager.Instance.UpdateBulletText(-1);
            AudioManager.Instance.PlaySound(AudioType.Error);
            return;
        }

        if (magazine > 0)
        {
            AddBullet(-1);
            GameObject g = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);

            g.GetComponent<Bullet>().StartMove(damage);
            
            AudioManager.Instance.PlaySound(AudioType.Fire);
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioType.NoMagazine);
        }
    }


    public void AddBullet(int amount)
    {
        magazine += amount;
        UIManager.Instance.UpdateBulletText(magazine);
    }
}
