﻿using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemyOneMovement : MonoBehaviour, EnemyMovementController
{
    // configurar esses valores dentro da Unity
    public float speed = 2f;
    public float range = 5f;
    private bool _locked;
    
    private Rigidbody2D _rigidbody2D;

    private Animator _animator;
    // deve ser setado para o objeto do player na Unity
    public GameObject target;


    public void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void Update() {
        if (!IsTimeToMove()) {
            _animator.SetFloat("Magnitude", 0f);
            _animator.SetFloat("X_axis", 0f);
            _animator.SetFloat("Y_axis", 0f);
        }
    }


    public void setMovementLock(bool lockMovement) {
        _locked = lockMovement;
    }

    public IEnumerator StartmovementCoolDown(float seconds) {
        setMovementLock(true);
        yield return new WaitForSeconds(seconds);
        setMovementLock(false);
    }

    public bool IsTimeToMove() {
        return !_locked && Vector3.Distance(transform.position, target.transform.position) < range;
    }

    public void Move() {
        follow(target.transform);
    }
    

    private void follow(Transform target) {
        Vector3 dir = target.transform.position - transform.position;
        
        _animator.SetFloat("Magnitude", dir.magnitude);
        dir.Normalize();
        _animator.SetFloat("X_axis", dir.x);
        _animator.SetFloat("Y_axis", dir.y);

        transform.position += dir * speed * Time.deltaTime;
    }
}
