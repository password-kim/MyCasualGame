using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public float damage = 1.0f;

    public float moveSpeed = 2.0f;

    public bool isHoming = false;

    private Transform myTransform = null;

    private Transform myTarget = null;

    private Vector3 direction = Vector3.zero;

    public void Init(Transform target, float damage, float speed, bool isHoming, Vector3 direction)
    {
        myTransform = GetComponent<Transform>();
        this.damage = damage;
        this.moveSpeed = speed;
        this.isHoming = isHoming;
        this.myTarget = target;

        if(myTarget != null)
        {
            this.direction = myTarget.position - myTransform.position;
            this.direction = this.direction.normalized;
        }
        else
        {
            this.direction = direction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget == null && direction == Vector3.zero)
            return;

        if (isHoming)
        {
            direction = myTarget.position - myTransform.position;
            direction = direction.normalized;
        }

        myTransform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // 화면에서 더이상 그려지지 않을때.
    private void OnBecameInvisible()
    {
        Debug.Log("OnBecameInvisible Called : Projectile");
        Destroy(gameObject);
    }
}


