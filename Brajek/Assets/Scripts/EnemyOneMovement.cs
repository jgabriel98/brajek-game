using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneMovement : MonoBehaviour
{
    public float speed = 2f;
    public float range = 5f;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      float distance = Vector3.Distance(transform.position, 
          target.transform.position);

      if (distance < range) {
        follow(target.transform);
      }
    }

    private void follow(Transform target) {
      Vector3 dir = target.transform.position - transform.position;
      dir.Normalize();

      transform.position += dir * speed * Time.deltaTime;
    }
}
