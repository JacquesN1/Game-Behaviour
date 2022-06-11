using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Shape {

    public override bool CollisionTest(Shape OtherCollider)
    {
        // if other collider is box, calculate AABB collision 
        if (OtherCollider.GetComponent<Box>() != null)
        {
            Vector2 colliderMin = new Vector2 (this.transform.position.x - (this.GetComponent<SpriteRenderer>().bounds.size.x / 2), this.transform.position.y - (this.GetComponent<SpriteRenderer>().bounds.size.y / 2));
            Vector2 colliderMax = new Vector2(this.transform.position.x + (this.GetComponent<SpriteRenderer>().bounds.size.x / 2), this.transform.position.y + (this.GetComponent<SpriteRenderer>().bounds.size.y / 2));
            Vector2 otherColliderMin = new Vector2(OtherCollider.transform.position.x - (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x / 2), OtherCollider.transform.position.y - (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.y / 2));
            Vector2 otherColliderMax = new Vector2(OtherCollider.transform.position.x + (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x / 2), OtherCollider.transform.position.y + (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.y / 2));

            if (((colliderMin.x <= otherColliderMax.x && colliderMax.x >= otherColliderMin.x)
                || (colliderMin.x >= otherColliderMax.x && colliderMax.x <= otherColliderMin.x))
                &&
                ((colliderMin.y <= otherColliderMax.y && colliderMax.y >= otherColliderMin.y)
                || (colliderMin.y >= otherColliderMax.y && colliderMax.y <= otherColliderMin.y)))
            {
                IsColliding = true;
            }
        }
        // if other collider is circle, calculate collision 
        else if (OtherCollider.GetComponent<Circle>() != null)
        {
            float colliderRadius = (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x) / 2;
            float circleX = OtherCollider.transform.position.x;
            float circleY = OtherCollider.transform.position.y;
            float rectangleLeft = this.transform.position.x - (this.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            float rectangleRight = this.transform.position.x + (this.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            float rectangleBottom = this.transform.position.y - (this.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            float rectangleTop = this.transform.position.y + (this.GetComponent<SpriteRenderer>().bounds.size.y / 2);

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
        if (OtherCollider.GetComponent<Box>() != null)
        {
            // Get the distance between the center points of the 2 colliders 
            Vector2 vectorDistance = this.transform.position - OtherCollider.transform.position;

            // remove negatives
            float y = System.Math.Abs(vectorDistance.y);
            float x = System.Math.Abs(vectorDistance.x);

            //check if the collison tacing place on the y axis 
            if (y / OtherCollider.GetComponent<SpriteRenderer>().bounds.size.y > x / OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x)
            {
                IsCollidingInXAxis = false;

                if (vectorDistance.y > 0) //if colliding with bottom
                {
                    //move to the bottom of the other colider
                    NewVector = new Vector2(this.transform.position.x, (OtherCollider.transform.position.y) + (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.y / 2) + (this.GetComponent<SpriteRenderer>().bounds.size.y / 2) + 0.01f);
                }
                else //if colliding with top
                {
                    //move to the top of the other colider
                    NewVector = new Vector2(this.transform.position.x, (OtherCollider.transform.position.y) - (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.y / 2) - (this.GetComponent<SpriteRenderer>().bounds.size.y / 2) - 0.01f);
                }
            }
            else //if colliding on x axis
            {
                IsCollidingInXAxis = true;

                if (vectorDistance.x > 0) //if colliding with right
                {
                    //move to the right of the other colider
                    NewVector = new Vector2((OtherCollider.transform.position.x) + (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x / 2) + (this.GetComponent<SpriteRenderer>().bounds.size.x / 2) + 0.01f, this.transform.position.y);
                }
                else //if colliding with left
                {
                    //move to the left of the other colider
                    NewVector = new Vector2((OtherCollider.transform.position.x) - (OtherCollider.GetComponent<SpriteRenderer>().bounds.size.x / 2) - (this.GetComponent<SpriteRenderer>().bounds.size.x / 2) - 0.01f, this.transform.position.y);
                }
            }
        }
        return  NewVector;
    }
}
