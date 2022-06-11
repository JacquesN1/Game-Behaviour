using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape{

    public override bool CollisionTest(Shape OtherCollider)
    {
        if (OtherCollider.GetComponent<Circle>() != null)
        {
            float colliderRadius = (this.GetComponent<SpriteRenderer>().bounds.size.x) / 2;
            float otherColliderRadius = (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x) / 2;

            var dx = this.transform.position.x - OtherCollider.transform.position.x;
            var dy = this.transform.position.y - OtherCollider.transform.position.y;
            var distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < colliderRadius + otherColliderRadius)
            {
                IsColliding = true;
            }
        }
        else if(OtherCollider.GetComponent<Box>() != null)
        {
            float colliderRadius = (this.GetComponent<SpriteRenderer>().bounds.size.x) / 2;
            float circleX = this.transform.position.x;
            float circleY = this.transform.position.y;
            float rectangleLeft = OtherCollider.transform.position.x - (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            float rectangleRight = OtherCollider.transform.position.x + (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            float rectangleBottom = OtherCollider.transform.position.y - (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            float rectangleTop = OtherCollider.transform.position.y + (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.y / 2);

            float closestX = Math.Min(Math.Max(circleX, rectangleLeft), rectangleRight);
            float closestY = Math.Min(Math.Max(circleY, rectangleBottom), rectangleTop);
            float distanceX = circleX - closestX;
            float distanceY = circleY - closestY;
            float distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

            if (distanceSquared < (colliderRadius * colliderRadius))
            {
                IsColliding = true;
            }
                   
        }
        return IsColliding;
    }

    public override Vector2 CollisonHandling(Shape OtherCollider)
    {
        NewVector = new Vector2(0, 0);
        return NewVector;
    }
}
