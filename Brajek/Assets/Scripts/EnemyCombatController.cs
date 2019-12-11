using UnityEngine;

//Interface para controlador de movimento de um inimigo
public interface EnemyCombatController
{
    bool isTimeToAttack();    //retorna se o inimigo deve atacar
    void Attack();    //método que faz inimigo atacar
    bool isInCoolDown();    //retorna se o ataque está em cooldown
}
