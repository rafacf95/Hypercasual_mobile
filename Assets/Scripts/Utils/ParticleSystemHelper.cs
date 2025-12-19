using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemHelper : MonoBehaviour
{
    public string tagToCompare = "Floor";
    private ParticleSystem _particles;

    void OnValidate()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    private void SetCollisionObject()
    {
        _particles.collision.AddPlane(GameObject.FindGameObjectWithTag(tagToCompare).GetComponent<Transform>().transform);
    }

    void Start()
    {
        SetCollisionObject();
    }
}
