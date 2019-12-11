using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

    //tinha criado isso antes para funciona como uma "interface" para os inimigos, mas acabou que nem era muito necessário...
public abstract class Enemy : MonoBehaviour
{
    
    public int Hp;
    protected EnemyMovementController _movementController;
    protected EnemyCombatController _combatController;

    // Update is called once per frame
    public void Update()
    {
        if (_movementController.IsTimeToMove()) {
            _movementController.Move();
        }
        if(_combatController.isTimeToAttack()) {
            _combatController.Attack();
        }
    }

    public void OnDestroy() {
        //animação do cara morrendo
    }
}
