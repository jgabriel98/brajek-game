using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

    //tinha criado isso antes para funciona como uma "interface" para os inimigos, mas acabou que nem era muito necessário...
public abstract class Enemy : MonoBehaviour
{
    public int Hp;
    public EnemyMovementController _movementController;
    public EnemyCombatController _combatController;

}
