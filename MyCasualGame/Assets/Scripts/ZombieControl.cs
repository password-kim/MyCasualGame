using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour
{
    public float moveSpeed = 4.0f;

    public GameObject target = null; // player.

    private SpriteRenderer spriteRenderer = null;

    private Transform myTransform;

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
        }
    }
}
