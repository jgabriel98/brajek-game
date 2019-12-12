using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyOneCombat: MonoBehaviour, EnemyCombatController
    {
        public GameObject target;
        public float minDistance = 2.0f;
        public float attackSpeed = 5.0f;
        private Rigidbody2D _rigidbody2D;

        private EnemyMovementController _thisMovementController;
        private float travelDistance {
            get { return minDistance + minDistance * 0.2f; }
        }
        public float recoverTime = 0.7f;    //tempo q fica imobilizado
        public float coolDownTime = 3.0f;    //tempo até poder atacar de novo
        private bool _isInCooldown;
        
        

        public void Start() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _thisMovementController = GetComponent<EnemyMovementController>();
            _isInCooldown = false;
        }

        public bool isTimeToAttack() {
            return !_isInCooldown && Vector2.Distance(target.transform.position, transform.position) <= minDistance;
        }

        public bool isInCoolDown() {
            return _isInCooldown;
        }

        public void Attack() {
            //pula pra cima do alvo, e tem um pequeno momento de recuperação
            StartCoroutine(ExecuteAttack());
        }

        IEnumerator ExecuteAttack() {
            _isInCooldown = true;
            
            //faz avanço rapido no alvo
            _rigidbody2D.velocity = (target.transform.position - transform.position).normalized * attackSpeed;
            float sec = travelDistance / _rigidbody2D.velocity.magnitude;
            yield return new WaitForSeconds(sec);
            
            _rigidbody2D.velocity = Vector2.zero;
            StartCoroutine(_thisMovementController.StartmovementCoolDown(recoverTime));
            
            yield return new WaitForSeconds(coolDownTime);    //inicia cooldown do ataque

            _isInCooldown = false;
        }

        public void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("colisor do inimigo foi ativado!");
        }
    }
}