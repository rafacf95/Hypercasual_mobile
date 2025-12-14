using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;
    public string tagEnemy = "Enemy";
    public string tagEndLine = "EndLine";

    public GameObject endScreen;

    private bool _canRun;
    private Vector3 _pos;
    void Update()
    {

        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(speed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagEnemy))
        {
            _canRun = false;
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagEndLine))
        {
            EndGame();
        }
    }

    public void StartRun()
    {
        _canRun = true;
    }

    public void EndGame()
    {
        endScreen.SetActive(true);
    }
}
