using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class combatController : MonoBehaviour
{
    private Camera camera;
    private Animator animator;
    public GameObject primaryAttackPrefab;
    public GameObject secondaryAttackPrefab;
    private GameObject primaryAttack_obj;
    private GameObject spriteRenderer;
    private GameObject attackCollider;

    public float attackDelay = 0.09f;    //duração em segundos
    public float attackDuration = 0.2f;    //duração em segundos
    public float projectileSpeed = 15f;
    private MovementControler  movementController;

    private static readonly Vector3[] CartesianPoints2D = {
        new Vector3(1, 0, 0), new Vector3(-1,0, 0),
        new Vector3(0, 1, 0), new Vector3(0,-1, 0)
    };

void Start() {
    camera = Camera.current ? Camera.current : Camera.main;
    movementController = gameObject.GetComponent<MovementControler>();
    animator = GetComponent<Animator>();
    primaryAttack_obj = Instantiate(primaryAttackPrefab, gameObject.transform);
    primaryAttack_obj.SetActive(false);
    spriteRenderer = primaryAttack_obj.transform.Find("AttackRenderer").gameObject;
    attackCollider = primaryAttack_obj.transform.Find("AttackCollider").gameObject;
}

    // Update is called once per frame
    void Update()
    {
        Vector3 aimDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aimDirection.z = 0;
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            //angle from two points: = Mathf.Atan2(p2.y-p1.y, p2.x-p1.x) * Mathf.Rad2Deg;
             StartCoroutine(atackPrimary(aimDirection));
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            attackSecondary(aimDirection);
        }
    }


    IEnumerator atackPrimary(Vector3 direction) {
        //verifica se está atacando
        if (!primaryAttack_obj.active) {
            Debug.Log("ataque primário na direção " + direction.ToString());
            Vector3 attackHitBoxDirection = direction.normalized;
            Vector3 attackSpritePosition = getClosestPointFrom(attackHitBoxDirection, CartesianPoints2D);
            
            /*por enquanto a area de colisão do ataque fica junta do local/direção da animação do ataque
            porém o objetivo final é que a direção/posição do ataque seja contínuo.
            nota: ( a animação deve continuar fixa em uma das 4 direções) 
            
            update: feito! - deixando comentário para entender melhor o que está acontecendo
            */

            spriteRenderer.transform.localPosition = attackSpritePosition;
            spriteRenderer.transform.rotation = Quaternion.FromToRotation(Vector3.up, attackSpritePosition);
            attackCollider.transform.localPosition = attackHitBoxDirection;
            attackCollider.transform.rotation = Quaternion.FromToRotation(Vector3.up, attackHitBoxDirection);

            movementController.isLocked = true;
            movementController.direction /= 5; //slow down na corrida quando for atacar
            
            animator.SetFloat("X_mouseAxis", attackHitBoxDirection.x);
            animator.SetFloat("Y_mouseAxis", attackHitBoxDirection.y);
            animator.SetTrigger("attack");
            yield return new WaitForSeconds(attackDelay*2);
            
            //faz pequeno deslize em direção ao ataque
            movementController.CurrentSpeed = 0.175f/attackDuration;
            movementController.direction = attackHitBoxDirection;
            primaryAttack_obj.SetActive(true);
            
            yield return new WaitForSeconds(attackDuration);
            movementController.CurrentSpeed = movementController.defaultSpeed;
            
            primaryAttack_obj.SetActive(false);
            movementController.isLocked = false;
        }
    }
    void attackSecondary(Vector3 direction) {
        Debug.Log("ataque secundário na direção "+ direction.ToString());
        GameObject tiro = Instantiate(secondaryAttackPrefab, transform.position, Quaternion.identity);
        tiro.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
    }

    Vector3 getClosestPointFrom(Vector3 pointFrom, Vector3[] pointsToLook) {
        Vector3 closest = pointsToLook[0];
        float minDistance = (closest - pointFrom).magnitude;
        
        for(int i=1; i<pointsToLook.Length; i++) {
            float nextDistance = (pointFrom - pointsToLook[i]).magnitude;
            if (nextDistance < minDistance) {
                minDistance = nextDistance;
                closest = pointsToLook[i];
            }
        }

        return closest;
    }

}
