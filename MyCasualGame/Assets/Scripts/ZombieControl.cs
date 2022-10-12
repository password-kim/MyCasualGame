using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour
{
    public float moveSpeed = 4.0f;

    public GameObject target = null; // player.

    private SpriteRenderer spriteRenderer = null;

    private Transform myTransform;

    // 뒤로 물러나는 기능. Backoff.
    private bool isBackOff = false;
    private float backOffTime = 0.0f;
    private Vector3 backOffDir = Vector3.zero;
    private float backOffSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        if(isBackOff == true)
        {
            if(backOffTime > 0.0f)
            {
                backOffTime -= Time.deltaTime;
                myTransform.Translate(backOffDir * Time.deltaTime * backOffSpeed);
            }
            else
            {
                isBackOff = false;
            }

            // 뒤로 밀리는 중에는 이동을 하지 않습니다. return !!
            return;
        }

        Vector3 direction = target.transform.position - myTransform.position;
        direction = direction.normalized;

        if(direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        myTransform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log($"zombie : player Hit");
        }

        if (collision.gameObject.tag.Equals("Projectile"))
        {
            Debug.Log($"zombie : projectile Hit = {collision.gameObject.name}");

            Vector3 dir = collision.gameObject.transform.position - myTransform.position;
            dir = dir.normalized;

            MoveBackOff(-dir, 0.2f, 3.0f);
        }
    }

    private void MoveBackOff(Vector3 dir, float time, float speed)
    {
        if (isBackOff)
        {
            return;
        }

        backOffDir = dir;
        backOffTime = time;
        backOffSpeed = speed;
        isBackOff = true;
    }
}
