using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> positions;
    public float duration = 1f;

    private int _index = 0;

    void Start()
    {

        if (!IsEmpty())
        {
            // transform.position = positions[RandomIndex()].transform.position;
            transform.position = positions[0].transform.position;
            NextIndex();

            StartCoroutine(StartMoviment());
        }
    }

    private void NextIndex()
    {
        _index++;
        if (_index >= positions.Count) _index = 0;
    }

    private int RandomIndex()
    {
        return Random.Range(0, positions.Count);
    }

    private bool IsEmpty()
    {
        return (positions.Count == 0);
    }

    IEnumerator StartMoviment()
    {
        if (IsEmpty()) yield break;

        float time = 0;

        while (true)
        {

            var curretnPosition = transform.position;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(curretnPosition, positions[_index].transform.position, (time / duration));

                time += Time.deltaTime;
                yield return null;
            }

            NextIndex();
            time = 0;

            yield return null;
        }
    }
}
