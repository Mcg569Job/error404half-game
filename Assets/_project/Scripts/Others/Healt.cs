using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healt : MonoBehaviour
{
    enum RobotType { Player, Enemy }

    [Header("Settings")]
    [SerializeField] private RobotType _type;
    [SerializeField] private float _maxHealt = 100;
    private float _healt;

    [Header("UI")]
    [SerializeField] private Image _bar;
    private void Start()
    {
        _healt = _maxHealt;
    }
    public void GetDamage(float amount)
    {
        _healt -= amount;

        if (_healt <= 0)
        {
            _healt = 0;
            Dead();
        }
        else if (_healt > _maxHealt)
            _healt = _maxHealt;

        _bar.fillAmount = _healt / _maxHealt;
    }


    private void Dead()
    {
        switch (_type)
        {
            case RobotType.Enemy:
                GetComponentInParent<Enemy>().Dead();
                GetDamage(-_maxHealt);
                break;
            case RobotType.Player:
                GetComponentInParent<RobotController>().Dead();
                break;
        }
    }
}
