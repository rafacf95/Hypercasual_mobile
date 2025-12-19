using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemHelper : MonoBehaviour
{
    public string tagToCompare = "Floor";

    private void SetCollisionObject()
    {
        GetComponent<ParticleSystem>().collision.AddPlane(GameObject.FindGameObjectWithTag(tagToCompare).GetComponent<Transform>().transform);
    }

    private void SetMaterial()
    {
        var parent = this.transform.parent;
        var parentMaterial = parent.GetComponentInChildren<MeshRenderer>().material;
        GetComponent<Renderer>().material = parentMaterial;
    }

    void Start()
    {
        SetCollisionObject();
        SetMaterial();
    }
}
