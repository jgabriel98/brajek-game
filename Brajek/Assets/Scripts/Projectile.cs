using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class Projectile : MonoBehaviour
{
    public float stunTime  = 0.25f;
    public float pushEnemyDuration = 0.4f;
    private Rigidbody2D _rigidbody;
    public void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            StartCoroutine(applyHitEffects(other));
        }
    }

    IEnumerator applyHitEffects(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
            
        Vector2 vel = (_rigidbody.mass/other.attachedRigidbody.mass) * (_rigidbody.velocity +_rigidbody.velocity.normalized);
        other.attachedRigidbody.velocity = vel;

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
    }

    IEnumerator stunEnemy(Rigidbody2D body, EnemyMovementController controller, float seconds) {
        body.velocity = Vector2.zero;
        yield return controller.StartmovementCoolDown(seconds);
    }
    
}
