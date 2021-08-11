using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    Rigidbody _rigidbody;
    AudioSource _source;
    private float v, h;
    private float _currentSpeed;

    [SerializeField] [Range(1, 10)] private float _speed = 2;
    [SerializeField] [Range(1, 10)] private float _rotSpeed = 2;
    [SerializeField] private Transform[] _wheels;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {

        if (ErrorManager.Instance.ErrorType != ErrorType.Direction)
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
        }
        else
        {
            v = -Input.GetAxis("Vertical");
            h = -Input.GetAxis("Horizontal");
        }

        if (ErrorManager.Instance.ErrorType != ErrorType.Motor)
        {
             _currentSpeed = _speed;
        }
        else
        {
             _currentSpeed = _speed / 2;
        }

        _rigidbody.velocity = transform.forward * _currentSpeed * v;
        transform.Rotate(0, h * _rotSpeed, 0);
        UpdateWheels();
    }

    private void UpdateWheels()
    {
        float fact = 4;
        if (v != 0)
        {
            _wheels[0].Rotate(v * _currentSpeed * fact, 0, 0);
            _wheels[1].Rotate(-v * _currentSpeed * fact, 0, 0);
            if (!_source.isPlaying) _source.Play();
        }
        else if (h != 0)
        {
            _wheels[0].Rotate(-h * _currentSpeed * fact, 0, 0);
            _wheels[1].Rotate(h * _currentSpeed * fact, 0, 0);
            if (!_source.isPlaying) _source.Play();
        }
        else
             if (_source.isPlaying) _source.Stop();

    }

    public void Dead()
    {
        FXManager.Instance.SpawnFX(FXType.Dead, transform.position);
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }

}
