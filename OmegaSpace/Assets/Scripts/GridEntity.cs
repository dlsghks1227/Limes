using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    [SerializeField] private Vector2 entitySize;

    private Vector2 pivot;
    private Vector2 edge;
    private Vector2 center;

    public Vector2 Size     { get=> entitySize; set=> entitySize = value; }
    public Vector2 Pivot    { get => pivot; }
    public Vector2 Edge     { get => edge; }
    public Vector2 Center   { get => center; }

    public Vector2 LocalPivot   
    { 
        get => new Vector2(
            -(entitySize.x * 0.5f - ((entitySize.x % 2.0f) == 0 ? 0.5f : 0.0f)), 
             (entitySize.y * 0.5f - ((entitySize.y % 2.0f) == 0 ? 0.5f : 0.0f))); 
    }
    public Vector2 LocalEdge
    {
        get => new Vector2(
             (entitySize.x * 0.5f + ((entitySize.x % 2.0f) == 0 ? 0.5f : 0.0f)),
            -(entitySize.y * 0.5f + ((entitySize.y % 2.0f) == 0 ? 0.5f : 0.0f)));
    }

    private void OnEnable()
    {
        pivot   = new Vector2(0.0f, 0.0f);
        edge    = new Vector2(entitySize.x, -entitySize.y);
        center  = new Vector2(pivot.x + (entitySize.x * 0.5f), pivot.y - (entitySize.y * 0.5f));
    }

    private bool IsValid()
    {
        return pivot != edge;
    }

    public void SetPosition(Vector2 position)
    {
        pivot   = new Vector2(position.x - (entitySize.x * 0.5f), position.y + (entitySize.y * 0.5f));
        edge    = new Vector2(position.x + (entitySize.x * 0.5f), position.y - (entitySize.y * 0.5f));
        center  = new Vector2(position.x, position.y);
        gameObject.transform.position = new Vector3(position.x, 0.0f, position.y);
    }

    public void SetPivotPosition(Vector2 gridPosition)
    {
        if (IsValid() == false)
        {
            pivot   = new Vector2(0.0f, 0.0f);
            edge    = new Vector2(entitySize.x, -entitySize.y);
            center  = new Vector2(pivot.x + (entitySize.x * 0.5f), pivot.y - (entitySize.y * 0.5f));
        }

        Vector2 offset = gridPosition - new Vector2(pivot.x + 0.5f, pivot.y - 0.5f);
        pivot   += offset;
        edge    += offset;
        center  += offset;
        gameObject.transform.position = new Vector3(center.x, 0.0f, center.y);
    }

    public void SetEdgePosition(Vector2 gridPosition)
    {
        if (IsValid() == false)
        {
            pivot   = new Vector2(0.0f, 0.0f);
            edge    = new Vector2(entitySize.x, -entitySize.y);
            center  = new Vector2(pivot.x + (entitySize.x * 0.5f), pivot.y - (entitySize.y * 0.5f));
        }

        Vector2 offset = gridPosition - new Vector2(edge.x - 0.5f, edge.y + 0.5f);
        pivot   += offset;
        edge    += offset;
        center  += offset;
        gameObject.transform.position = new Vector3(center.x, 0.0f, center.y);
    }

    public void SetCenterPosition(Vector2 gridPosition)
    {
        if (IsValid() == false)
        {
            pivot   = new Vector2(0.0f, 0.0f);
            edge    = new Vector2(entitySize.x, -entitySize.y);
            center  = new Vector2(pivot.x + (entitySize.x * 0.5f), pivot.y - (entitySize.y * 0.5f));
        }

        Vector2 offset = gridPosition - new Vector2(
            center.x - ((entitySize.x % 2.0f) == 0 ? 0.5f : 0.0f),
            center.y + ((entitySize.y % 2.0f) == 0 ? 0.5f : 0.0f));
        pivot   += offset;
        edge    += offset;
        center  += offset;
        gameObject.transform.position = new Vector3(center.x, 0.0f, center.y);
    }

    public void Rotate(float angle)
    {
        if (IsValid() == false)
        {
            pivot   = new Vector2(0.0f, 0.0f);
            edge    = new Vector2(entitySize.x, -entitySize.y);
            center  = new Vector2(pivot.x + (entitySize.x * 0.5f), pivot.y - (entitySize.y * 0.5f));
        }

        entitySize = new Vector2(entitySize.y, entitySize.x);

        pivot   = new Vector2(0.0f, 0.0f);
        edge    = new Vector2(entitySize.x, -entitySize.y);
        center  = new Vector2(pivot.x + (edge.x * 0.5f), pivot.y + (edge.y * 0.5f));

        gameObject.transform.Rotate(0.0f, angle, 0.0f);
    }
}