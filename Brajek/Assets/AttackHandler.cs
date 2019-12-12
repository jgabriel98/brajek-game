using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public float stunTime  = 0.25f;
    public float pushEnemyDuration = 0.4f;
    public Vector2 attackDirection;
    
    public void Start() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            StartCoroutine(applyHitEffects(other));
        }
    }
    
    IEnumerator applyHitEffects(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy._combatController.isInvencible()) yield break;
        
        //enemy._combatController.setInvencible(1);

        var attachedRigidbody = other.attachedRigidbody;
        Vector2 vel = (attackDirection).normalized * ((0.1f/attachedRigidbody.mass));
        attachedRigidbody.velocity = vel;

        yield return pushEnemy(other.attachedRigidbody, enemy._movementController, vel, pushEnemyDuration);

        if (--enemy.Hp <= 0) {
            Destroy(enemy.gameObject);
            yield break;
        }
        
        yield return stunEnemy(other.attachedRigidbody, enemy._movementController, stunTime);
        
    }
    
    IEnumerator pushEnemy(Rigidbody2D body, EnemyMovementController controller, Vector2 velocity, float duration) {
        body.velocity = velocity;
        yield return controller.StartmovementCoolDown(duration);
        body.velocity = Vector2.zero;
    }

    IEnumerator stunEnemy(Rigidbody2D body, EnemyMovementController controller, float seconds) {
        body.velocity = Vector2.zero;
        yield return controller.StartmovementCoolDown(seconds);
    }
}
