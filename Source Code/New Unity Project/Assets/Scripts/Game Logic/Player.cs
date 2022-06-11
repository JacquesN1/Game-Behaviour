using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PhysicsObject _player;
    public PhysicsObject ghost;
    public PhysicsObject ghost2;

    public int playerSpeed = 30;
    public float moveX;
    public float moveY;

    public int health = 15;
    public int coins = 0;

    private Vector2 velocity;
    private float mass;

    // Use this for initialization
    void Start()
    {
        mass = _player.GetComponent<RBody>().GetMass();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = _player.GetComponent<RBody>().GetVelocity();
        PlayerMove();

        //if player is colliding with enemy game over
        if (CollisionTest(ghost.shape) == true || CollisionTest(ghost2.shape) == true)
        {
            Application.Quit();
        }
    }

    void PlayerMove()
    {
        if (Input.GetButton("Horizontal"))
        {
            // x-axis movement
            moveX = Input.GetAxis("Horizontal");
            _player.GetComponent<RBody>().ApplyForce(new Vector2(moveX * playerSpeed, (velocity.y * mass)));
        }
        if (Input.GetButton("Vertical"))
        {
            // y-axis movement
            moveY = Input.GetAxis("Vertical");
            _player.GetComponent<RBody>().ApplyForce(new Vector2((velocity.x * mass), moveY * playerSpeed));
        }
    }

    //AABB Collision to test weather the player is touching the ghost
    public bool CollisionTest(Shape OtherCollider)
    {
        bool IsColliding = false;

        // calculate AABB collision 
        Vector2 colliderMin = new Vector2(this.transform.position.x - (this.GetComponent<SpriteRenderer>().bounds.size.x / 2), this.transform.position.y - (this.GetComponent<SpriteRenderer>().bounds.size.y / 2));
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
        return IsColliding;
    }
}
