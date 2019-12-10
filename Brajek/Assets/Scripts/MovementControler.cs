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

    public Vector3 direction;
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
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
            //para andar sempre na mesma velocidade, independente da direção (ex.: não andar super rapido na diagonal)
            direction.Normalize();
    
            animator.SetFloat("X_axis", direction.x);
            animator.SetFloat("Y_axis", direction.y);
            animator.SetFloat("Magnitude", direction.magnitude);
            
            Move(direction);
        }
        else {
            Move(direction);
        }
    }

    public void Move(Vector3 movement) {
        transform.position += movement * Time.deltaTime * _currentSpeed;
    }
    
    
}
