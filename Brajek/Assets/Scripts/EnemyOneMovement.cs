﻿using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemyOneMovement : MonoBehaviour, EnemyMovementController
{
    // configurar esses valores dentro da Unity
    public float speed = 2f;
    public float range = 5f;
    private bool _locked;

    // deve ser setado para o objeto do player na Unity
    public GameObject target;


    public void setMovementLock(bool lockMovement) {
        _locked = lockMovement;
    }
    
    public bool IsTimeToMove() {
        return !_locked && Vector3.Distance(transform.position, target.transform.position) < range;
    }

    public void Move() {
        follow(target.transform);
    }
    

    private void follow(Transform target) {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        transform.position += dir * speed * Time.deltaTime;
    }
}
