using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    private Transform myTransform;
    private SpriteRenderer myRenderer;

    public enum ProjectileType
    {
        None = -1,
        Bullet,     // 0
        Missile,    // 1
        Homing,     // 2

        MAX,        // 3
    }

    public ProjectileType projectileType = ProjectileType.None;
    public float shootDelay = 0.5f;
    private float lastShootTime = 0.0f;
    private float damage = 0f;
    private float moveSpeed = 0f;
    private bool isHoming = false;

    public GameObject[] projectilePrefabs = new GameObject[(int)ProjectileType.MAX];
    
    public void ShootProjectile(ProjectileType projectileType, float damage, float speed, bool isHoming, float shootDelay)
    {
        this.projectileType = projectileType;
        this.damage = damage;
        this.moveSpeed = speed;
        this.isHoming = isHoming;
        this.shootDelay = shootDelay;
    }

    private Transform FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets == null || targets.Length == 0)
            return null;

        float minDistance = 10000f;
        Transform newTarget = targets[0].transform;
        foreach(GameObject target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, myTransform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                newTarget = target.transform;
            }
        }

        return newTarget;
    }

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRenderer = GetComponent<SpriteRenderer>();
        lastShootTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileType == ProjectileType.None)
            return;

        if(lastShootTime + shootDelay < Time.time)
        {
            lastShootTime = Time.time;
            Transform target = FindTarget();

            GameObject newProjectile = Instantiate(projectilePrefabs[(int)projectileType], myTransform.position, Quaternion.identity);
            ProjectileControl projectileControl = newProjectile.GetComponent<ProjectileControl>();
            

            if (target == null)
            {
                float horizontal = Input.GetAxis("Horizontal"); // 좌우 화살표 키 값. -1 ~ 1
                float vertical = Input.GetAxis("Vertical"); // 상하 화살표 키 값. -1 ~ 1

                Vector3 projectileDirection = horizontal * Vector3.right + vertical * Vector3.up;
                projectileDirection = projectileDirection.normalized;

                if (projectileDirection == Vector3.zero)
                {
                    if (myRenderer.flipX == false)
                    {
                        projectileDirection = Vector3.right;
                    }
                    else
                    {
                        projectileDirection = Vector3.left;
                    }
                }

                projectileControl.Init(null, damage, moveSpeed, isHoming, projectileDirection);
            }
            else
            {
                projectileControl.Init(target, damage, moveSpeed, isHoming, Vector3.zero);
            }
        }
    }
}
