using UnityEngine;

//Interface para controlador de movimento de um inimigo
public interface EnemyCombatController
{
    /*deixa invencível por um tempo,
    *1 para deixar invencivel por tempo indeterminado ( até chamar esse método mais uma vez com um valor de tempo >=0)
     */
    void setInvencible(float seconds);

    bool isInvencible();
    bool isTimeToAttack();    //retorna se o inimigo deve atacar
    void Attack();    //método que faz inimigo atacar, e supostamente faz o ataque entrar em cooldown
    bool isInCoolDown();    //retorna se o ataque está em cooldown
}
