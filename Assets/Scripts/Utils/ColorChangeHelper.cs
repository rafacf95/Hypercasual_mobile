using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChangeHelper : MonoBehaviour
{
    public float duration = 2f;
    public MeshRenderer meshRenderer;
    public Color color = Color.white;

    private Color _startColor;

    void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        _startColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", color);
        meshRenderer.materials[0].DOColor(_startColor, duration).SetDelay(.5f);
    }
}
