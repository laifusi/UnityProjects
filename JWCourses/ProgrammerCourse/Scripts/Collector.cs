using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> collectibles;
    [SerializeField] UnityEvent onCollectionComplete;

    TMP_Text remainingText;
    int countCollected;

    Color selectedGizmoColor = Color.red;
    Color nonSelectedGizmoColor = new Color(1, 0, 0, 0.1f);

    // Start is called before the first frame update
    void Start()
    {
        remainingText = GetComponentInChildren<TMP_Text>();
        foreach (var collectible in collectibles)
        {
            collectible.OnPickedUp += ItemPickedUp;
        }

        int countRemaining = collectibles.Count - countCollected;
        remainingText?.SetText(countRemaining.ToString());
    }

    void ItemPickedUp()
    {
        countCollected++;

        int countRemaining = collectibles.Count - countCollected;
        remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0)
            return;

        onCollectionComplete.Invoke();
    }

    void OnValidate()
    {
        collectibles = collectibles.Distinct().ToList();
    }

    void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
            Gizmos.color = selectedGizmoColor;
        else
            Gizmos.color = nonSelectedGizmoColor;

        foreach (var collectible in collectibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }
}
