using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float MoveSpeed = 8.0f;

    private Transform myTransform = null;

    private SpriteRenderer myRenderer = null;

    private ProjectileShooter shooter = null;

    public HPBarControl hpBar = null;

    public int HP = 300;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRenderer = GetComponent<SpriteRenderer>();

        // projectile shooter
        shooter = GetComponent<ProjectileShooter>();
        shooter.ShootProjectile(ProjectileShooter.ProjectileType.Bullet, 5f, 10f, false, 1.0f);

        hpBar.SetMaxHp(HP);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // �¿� ȭ��ǥ Ű ��. -1 ~ 1
        float vertical = Input.GetAxis("Vertical"); // ���� ȭ��ǥ Ű ��. -1 ~ 1

        Vector3 moveDirection = horizontal * Vector3.right + vertical * Vector3.up;
        moveDirection = moveDirection.normalized;

        if(horizontal > 0.0f)
        {
            myRenderer.flipX = false;
        }
        else if(horizontal < 0.0f)
        {
            myRenderer.flipX = true;
        }

        myTransform.Translate(moveDirection * MoveSpeed * Time.deltaTime);
    }

    // ����Ϳ� �浹�ϸ� ���� ������Ʈ�� sendMessage�� �������� �˷��ش�.
    private void OnHitDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Max(HP, 0);
        hpBar.SetHP(HP);
        Debug.Log($"Damage : {damage}");
    }
}
