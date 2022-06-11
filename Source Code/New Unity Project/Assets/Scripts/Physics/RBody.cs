using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shape))]
public class RBody : MonoBehaviour {

    Vector2 position;
    Vector2 velocity;

    float gravity;
    public float mass;
    public bool enemy = false;

    public Vector2 GetPosition()
    {
        return position;
    }

    public float GetMass()
    {
        return mass;
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }

    public float GetGravity()
    {
        return gravity;
    }

    public void SetPosition(Vector2 _position)
    {
        position = _position;
    }

    // Update is called once per frame
    public void UpdatePosition()
    {
        // calculate gravity effect on velocity, and update position
        if (mass != 0)
        { 
            velocity.y = velocity.y + (gravity * Time.deltaTime);
            position.x = position.x + (velocity.x * Time.deltaTime);
            position.y = position.y + (velocity.y * Time.deltaTime);
        }
    }

    public void ApplyForce(Vector2 force)
    {
        velocity = new Vector2(force.x / mass, force.y / mass);
    }

    public void SetMass(float _mass)
    {
        mass = _mass;
    }
      
    public void SetGravity(float _gravity)
    {
        gravity = _gravity;
    }
}
