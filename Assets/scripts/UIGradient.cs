using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Adds a smooth color gradient to any UI Graphic (Image, etc.) by directly
/// modifying its vertex colors. No external image file needed.
/// Attach this to the same GameObject as your background Image.
/// Uses Unity's documented BaseMeshEffect pattern (not raw IMeshModifier).
/// </summary>
[RequireComponent(typeof(Graphic))]
public class UIGradient : BaseMeshEffect
{
    public Color topColor = new Color(0.10f, 0.16f, 0.25f);    // dark blue
    public Color bottomColor = new Color(0.35f, 0.55f, 0.70f);  // soft light blue

    [Range(0f, 180f)]
    public float angle = 0f; // 0 = top-to-bottom, 90 = left-to-right

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive() || vh.currentVertCount == 0) return;

        UIVertex vertex = default;
        Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 max = new Vector2(float.MinValue, float.MinValue);

        // Find bounds of the mesh
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);
            min = Vector2.Min(min, vertex.position);
            max = Vector2.Max(max, vertex.position);
        }

        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
        dir.Normalize();

        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);

            Vector2 normalizedPos = new Vector2(
                max.x > min.x ? Mathf.InverseLerp(min.x, max.x, vertex.position.x) : 0.5f,
                max.y > min.y ? Mathf.InverseLerp(min.y, max.y, vertex.position.y) : 0.5f
            );

            float t = Vector2.Dot(normalizedPos, dir);
            t = Mathf.Clamp01(t);

            vertex.color = Color.Lerp(bottomColor, topColor, t);
            vh.SetUIVertex(vertex, i);
        }
    }
}