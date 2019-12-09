using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{
    private Animator animator;
    public float speed;

    public bool isLocked = false;
    //public CharacterController controller;

    public Vector3 movement;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (isLocked)    //TODO: será que aqui preciso setar "X_axis", "Y_axis" e "Magnitude" para 0?
            return;
        
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        //para andar sempre na mesma velocidade, independente da direção (ex.: não andar super rapido na diagonal)
        movement.Normalize();

        animator.SetFloat("X_axis", movement.x);
        animator.SetFloat("Y_axis", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        
        transform.position += movement * Time.deltaTime * speed;
    }
}
