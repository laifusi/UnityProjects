using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridNumbers : MonoBehaviour
{
#if UNITY_EDITOR
    public Vector2Int GridSize { get; set; }

    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.fixedHeight = 1;
        style.fixedWidth = 1;
        style.alignment = TextAnchor.MiddleCenter;
        style.richText = true;
        for (int i = -GridSize.x + 2; i < GridSize.x - 1; i++)
            for (int j = -GridSize.y + 2; j < GridSize.y - 1; j++)
                Handles.Label(new Vector3(i, j, 0), "<color=red>" + i + ", " + j + "</color>", style);
    }

    [ContextMenu("GetGridSize")]
    private void GetGridSize()
    {
        Vector2 size = GetComponent<SpriteRenderer>().size;
        GridSize = new Vector2Int((int)size.x - 2, (int)size.y - 2);
    }
#endif
}
