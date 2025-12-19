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

    [Header("Player Configs")]
    public float speed = 1f;
    public string tagEnemy = "Enemy";
    public string tagEndLine = "EndLine";
    public float SideLimit = 4f;
    public Vector2 vectorSideLimit = new Vector2(-4, 4);

    [Header("Power Ups")]
    public bool invincible;
    public TextMeshPro textMeshPro;
    public GameObject coinCollector;

    [Header("Animation setup")]
    public AnimatorManager animatorManager;
    public float spawnDuration = .5f;
    public Ease ease = Ease.OutBack;

    [Header("Particles setup")]
    public ParticleSystem vfxDeath;

    [SerializeField] private BounceHelper _bounceHelper;

    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseAnimationSpeed = 7f;

    void Start()
    {
        _startPosition = transform.position;
        // ResetSpeed();
        _currentSpeed = speed;
        SetPowerUpText();
        SpawnAnimation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnAnimation();
        }

        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        // if (_pos.x < -SideLimit) _pos.x = -SideLimit;
        // else if (_pos.x > SideLimit) _pos.x = SideLimit;

        if (_pos.x < vectorSideLimit.x) _pos.x = vectorSideLimit.x;
        else if (_pos.x > vectorSideLimit.y) _pos.x = SideLimit;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(_currentSpeed * Time.deltaTime * transform.forward);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagEnemy) && !invincible)
        {
            _canRun = false;
            transform.DOMoveZ(-1f, .3f).SetRelative();
            GameManager.Instance.EndGame();
            animatorManager.Play(AnimatorManager.AnimationType.DEAD);
            SetPowerUpText();

            if (!vfxDeath.isPlaying) vfxDeath.Play();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagEndLine))
        {
            _canRun = false;
            GameManager.Instance.EndGame();
            animatorManager.Play(AnimatorManager.AnimationType.IDLE);
        }
    }

    public void StartRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN);
    }

    public void Bounce()
    {
        if (_bounceHelper != null)
        {
            _bounceHelper.Bounce();
        }
    }

    public void SpawnAnimation()
    {
        transform.DOScale(0, spawnDuration).SetEase(ease).From().OnComplete(() =>
        {
            transform.localScale = Vector3.one;
        });
    }

    #region PowerUps
    public void SetPowerUpText(string s = "")
    {
        textMeshPro.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseAnimationSpeed);
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseAnimationSpeed);
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

    public void ChangeCoinCollectorSize(float size)
    {
        coinCollector.transform.localScale = Vector3.one * size;
    }
    #endregion


}
