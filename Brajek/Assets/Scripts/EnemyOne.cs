using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    public int Hp = 1;
    public EnemyMovementController _movementController;
    public EnemyCombatController _combatController;
    
    // Start is called before the first frame update
    void Start() {
        this._movementController = GetComponent<EnemyOneMovement>();
        this._combatController = GetComponent<EnemyOneCombat>();
    }

    // Update is called once per frame
    public void Update() {
        if(_combatController.isTimeToAttack()) {
            _combatController.Attack();
        } else if (_movementController.IsTimeToMove()) {
            _movementController.Move();
        }
            
    }
}
