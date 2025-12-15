using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;
using DG.Tweening;

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
    public TextMeshPro textMeshPro;

    private bool _canRun;
    private Vector3 _pos;
    [SerializeField] private float _currentSpeed;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
        SetPowerUpText();
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

    #region PowerUps

    public void SetPowerUpText(string s = "")
    {
        textMeshPro.text = s;
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

    public void ChangeHeight(float height, float duration, float animationDuration, Ease ease)
    {
        // var p = transform.position;
        // p.y = _startPosition.y + height;
        // transform.position = p;
        transform.DOMoveY(_startPosition.y + height, animationDuration).SetEase(ease);
    }

    public void ResetHeight()
    {
        // var p = transform.position;
        // p.y = _startPosition.y;
        // transform.position = p;
        transform.DOMoveY(_startPosition.y, .1f);
    }

    #endregion
}
