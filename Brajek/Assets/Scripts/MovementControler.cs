using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{
    private Animator animator;
    public float defaultSpeed;
    private float _currentSpeed;

    public float CurrentSpeed
    {
        get => _currentSpeed;
        set => _currentSpeed = value;
    }

    public bool isLocked;
    //public CharacterController controller;

    public Vector3 movement;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        CurrentSpeed = defaultSpeed;
        isLocked = false;
    }

    // Update is called once per frame
    void Update() {
        //TODO: será que aqui preciso setar "X_axis", "Y_axis" e "Magnitude" para 0?
        if (!isLocked) {
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
            //para andar sempre na mesma velocidade, independente da direção (ex.: não andar super rapido na diagonal)
            movement.Normalize();
    
            animator.SetFloat("X_axis", movement.x);
            animator.SetFloat("Y_axis", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);
            
            Move(movement);
        }
    }

    public void Move(Vector3 movement) {
        transform.position += movement * Time.deltaTime * _currentSpeed;
    }
    
    
}
