using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{

    public Animator animator;
    public float speed;
    //public CharacterController controller;

    public Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.Normalize();

        animator.SetFloat("X_axis", movement.x);
        animator.SetFloat("Y_axis", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        
        transform.position += movement * Time.deltaTime * speed;
    }
}
