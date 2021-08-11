using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody _rigidbody;
    Animator _animator;
    Transform _target = null;
    Healt _targetHealt = null;


    [Header("Speed")]
    [SerializeField] [Range(1, 5)] private float _speed = 2;

    [Header("Attack Settings")]
    [SerializeField] [Range(1, 100)] private int _damage = 5;
    [SerializeField] [Range(1, 10)] private float _attackTime = 2;
    [SerializeField] [Range(.1f, 5)] private float _approachDistance = 1;
    private float _currentTime;
    [Header("FX")]
    [SerializeField] private GameObject _attackFx;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _currentTime = 0;
        _animator.speed = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _target = other.transform;
            _targetHealt = _target.GetComponentInChildren<Healt>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _target = null;
            _targetHealt = null;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameOver) return;

        if (_target == null)
        {
            if (_animator.speed == 1)
                _animator.speed = 0;

            return;
        }

        Vector3 lookPos = _target.position - transform.position;
        lookPos.y = 0;
        Quaternion rot = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, .125f);

        float distance = Vector3.Distance(transform.position, _target.position);
        if (distance <= _approachDistance)
        {
            Attack();
        }
        else
        {
            if (_attackFx.activeSelf)
                _attackFx.SetActive(false);

            if (_animator.speed == 0)
                _animator.speed = 1;

            _rigidbody.velocity = transform.forward * _speed;
        }


    }

    private void HideFx() => _attackFx.SetActive(false);

    private void Attack()
    {
        Debug.DrawRay(transform.position, transform.forward * (_approachDistance + .1f), Color.green);

        if (_animator.speed == 1)
            _animator.speed = 0;

        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            _currentTime = _attackTime;
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        _attackFx.SetActive(true);
        Invoke("HideFx", _attackTime / 2);
        yield return new WaitForSeconds(.3f);
        
        RaycastHit hit;
        bool ray = Physics.Raycast(transform.position, transform.forward, out hit, _approachDistance + .1f);
        if (ray)
        {
            if (hit.transform.tag == "Player")
            {
                int damage = _damage + (GameManager.Instance.Difficulty);
                _targetHealt.GetDamage(damage);
            }
        }
    }

    public void Dead()
    {
        _target = null;
        GameManager.Instance.AddScore(1);
        FXManager.Instance.SpawnFX(FXType.Dead, transform.position);
        gameObject.SetActive(false);
    }
}
