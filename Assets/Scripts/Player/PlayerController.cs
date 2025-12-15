using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;
    public string tagEnemy = "Enemy";
    public string tagEndLine = "EndLine";

    [Header("Power Ups")]
    public bool invincible;

    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(_currentSpeed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagEnemy) && !invincible)
        {
            _canRun = false;
            GameManager.Instance.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagEndLine))
        {
            GameManager.Instance.EndGame();
        }
    }

    public void StartRun()
    {
        _canRun = true;
    }

    public void SetPowerUpText(string s)
    {
        // uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvincible(bool b = true)
    {
        invincible = b;
    }
}
