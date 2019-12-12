using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface para controlador de movimento de um inimigo
public interface EnemyMovementController
{
    void Move();    //realiza ação de movimento do inimigo ( seja lá como vc quer que ele seja)
    
    bool IsTimeToMove();    //indica se deve se mover agora

    
    //Bloqueia movimento por um tempo, tem o tempo efeito que chamar setMovementLock(true) esperar o tempo padrão e então setMovementLock(false)
    IEnumerator StartmovementCoolDown(float seconds);
    
    void setMovementLock(bool lockMovement);    //bloqueia movimento ( normalmente usado quando alguma outra ação vai ser feita)
}
