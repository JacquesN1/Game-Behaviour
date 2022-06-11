using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    float gravity;

    // Use this for initialization
    void Start()
    {
        gravity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PhysicsObject ObjA in ObjectList.Objects)
        {
            ObjA.shape.IsColliding = false;
            // Get the left and right sides of the physics object
            float ObjALeft = ObjA.transform.position.x - (ObjA.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            float ObjARight = ObjA.transform.position.x + (ObjA.GetComponent<SpriteRenderer>().bounds.size.x / 2);

            foreach (PhysicsObject ObjB in ObjectList.Objects)
            {
                if (ObjA != ObjB)
                {
                    if (ObjA.shape.IsColliding == false )
                    {
                        // check if the left and right x values overap
                        float ObjBLeft = ObjB.transform.position.x - (ObjB.GetComponent<SpriteRenderer>().bounds.size.x / 2);
                        float ObjBRight = ObjB.transform.position.x + (ObjB.GetComponent<SpriteRenderer>().bounds.size.x / 2);

                        if ((ObjBLeft >= ObjALeft && ObjBLeft <= ObjARight) || (ObjBRight >= ObjALeft && ObjBRight <= ObjARight) || (ObjBLeft <= ObjALeft && ObjBRight >= ObjARight) || (ObjBLeft >= ObjALeft && ObjBRight <= ObjARight))
                        {
                            //Dont test if both obejects are non moving
                            if (!(ObjA.rBody.mass == 0 && ObjA.rBody.mass == 0))
                            {
                                //if they overlap, do full aabb collision test
                                ObjA.shape.CollisionTest(ObjB.shape);
                            }

                            if (ObjA.shape.IsColliding == true && ObjA.rBody.mass != 0)
                            {
                                //if they collide run collision handaling script and move to result.
                                ObjA.rBody.SetPosition(ObjA.shape.CollisonHandling(ObjB.shape));

                                // Apply bounce force after collision depending on direction of collision
                                if (ObjA.shape.IsCollidingInXAxis == true)
                                {
                                    Vector2 velocity = (ObjA.rBody.GetVelocity()) / 2;
                                    velocity.x = velocity.x * -1;
                                    ObjA.rBody.ApplyForce(velocity);
                                }
                                else 
                                {
                                    Vector2 velocity = (ObjA.rBody.GetVelocity()) / 2;
                                    velocity.y = velocity.y * -1;
                                    ObjA.rBody.ApplyForce(velocity);
                                }
                                
                            }
                        }
                        
                    }
                }
            }
            //apply gravity
            if (ObjA.shape.IsColliding == false)
            {
                ObjA.rBody.SetGravity(gravity);
                ObjA.rBody.UpdatePosition();
            }
        }
    }
}
