using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemyOne : Enemy
{
    public int Hp
    {
        get => base.Hp;
        set => base.Hp = value;
    }
    public EnemyMovementController _movementController{
        get => base._movementController;
        set => base._movementController = value;
    }
    public EnemyCombatController _combatController{
        get => base._combatController;
        set => base._combatController = value;
    }
    
    // Start is called before the first frame update
    void Start() {
        this._movementController = GetComponent<EnemyOneMovement>();
        this._combatController = GetComponent<EnemyOneCombat>();
        Hp = 3;
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
