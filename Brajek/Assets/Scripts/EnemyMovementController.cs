using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface para controlador de movimento de um inimigo
public interface EnemyMovementController
{
    void Move();    //realiza ação de movimento do inimigo ( seja lá como vc quer que ele seja)
    bool IsTimeToMove();    //indica se deve se mover agora
    
    void setMovementLock(bool lockMovement);    //bloqueia movimento ( normalmente usado quando alguma outra ação vai ser feita)
}
