using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : MonoBehaviour
{
    public bool IsColliding = false;
    public bool IsCollidingInXAxis;
    public Vector2 NewVector;

    public abstract bool CollisionTest(Shape OtherShape);

    public abstract Vector2 CollisonHandling(Shape OtherShape);

    public virtual void Update()
    {
        if (IsColliding)
        {
            IsColliding = true;
        }
        else
        {
            IsColliding = false;
        }

    }
    public virtual void Start()
    {
    }
}
