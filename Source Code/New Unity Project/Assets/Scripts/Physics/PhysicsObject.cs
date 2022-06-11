using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public RBody rBody;
    public Shape shape;
    public Material material;

    private Vector2 force;

    public PhysicsObject(RBody _rBody, Shape _shape, Material _material)
    {
        rBody = _rBody;
        shape = _shape;
        material = _material;
    }

    // Use this for initialization
    void Awake() 
    {
        ObjectList.Objects.Add(this);
        rBody.SetPosition(this.transform.position);
        force = new Vector2(0.0f, 0.0f);
        rBody.ApplyForce(force);
    }

	// Update is called once per frame
	void Update ()
    {
        this.transform.position = rBody.GetPosition();
    }
}
