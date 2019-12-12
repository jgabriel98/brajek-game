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
        private SpriteRenderer _spriteRenderer;
        private float travelDistance {
            get { return minDistance + minDistance * 0.2f; }
        }
        public float recoverTime = 0.7f;    //tempo q fica imobilizado
        public float coolDownTime = 3.0f;    //tempo até poder atacar de novo
        private bool _isInCooldown;
        private bool _isInvencible;
        
        
        public void Start() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _thisMovementController = GetComponent<EnemyMovementController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _isInCooldown = false;
            _isInvencible = false;
        }

        public bool isTimeToAttack() {
            return !_isInCooldown && Vector2.Distance(target.transform.position, transform.position) <= minDistance;
        }

        public bool isInCoolDown() { return _isInCooldown; }
        public bool isInvencible() { return _isInvencible; }

        public void setInvencible(float seconds) {
            if (seconds < 0) {
                _isInvencible = true;
            }
            else { 
                StartCoroutine(setInvencibleForTime(seconds));
            }
        }

        public void Attack() {
            //pula pra cima do alvo, e tem um pequeno momento de recuperação
            StartCoroutine(ExecuteAttack());
        }

        IEnumerator setInvencibleForTime(float seconds)
        {
            Color blinkColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            int n = 2*(int)seconds;    //pisca a cada 0.25 segundo
            _isInvencible = true;
            for (int i = 0; i < n; i++)
            {
                _spriteRenderer.color = blinkColor;
                yield return new WaitForSeconds(0.25f);
                _spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.25f);
            }

            _isInvencible = false;

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