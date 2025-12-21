using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    [Header("Configuration")]
    public string compareTag = "Player";
    public ParticleSystem particles;
    public float timeToHide = 1f;
    public GameObject graphItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }

    public void HideGraph()
    {
        if (graphItem != null) graphItem.SetActive(false);
    }

    protected virtual void Collect()
    {
        // Debug.Log("Collect");
        HideGraph();
        Invoke(nameof(HideObject), timeToHide);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        // Debug.Log("OnCollect");
        if (particles != null)
        {
            if (!particles.isPlaying)
            {
                // particles.collision.AddPlane(GameObject.Find("SPR_Floor").GetComponent<Transform>());
                // particles.collision.AddPlane(GameObject.FindGameObjectWithTag("Floor").GetComponent<Transform>().transform);
                particles.Play();
            }
        }
        if(audioSource != null) audioSource.Play();
    }

}
