using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask shootLayer;
    private bool isReloaded = false;
    private Attack attack;
    private bool canMoveRight = false;
   private  Transform playerTransform;
    private void Awake()
    {
        attack = GetComponent<Attack>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        findThePlayer(playerTransform);
        EnemyAttack();
        CheckCanMoveRight();
        MoveTowards();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Reload()
    {
        attack.GetAmmo = attack.MaxAmmoCount;
        isReloaded = false;
    }
    private void EnemyAttack()
    {
        if (attack.GetAmmo <= 0 && isReloaded == false)
        {
            Invoke(nameof(Reload), 5f);
            isReloaded = true;
        }
        if (attack.GetCurrentFireRate <= 0f && attack.GetAmmo > 0f && attack && findThePlayer(playerTransform))
        {
            attack.FireEnemy();
        }
    }
    float viewDistance = 12f;
    float viewAngle = 65f;
    bool findThePlayer(Transform playerPos)
    {
        float distance= transform.position.y -playerPos.position.y;
        Mathf.Abs(distance);
        if (Vector3.Distance(transform.position, playerPos.position) < viewDistance)
        {
            Vector3 directionToPlayer = (playerPos.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle && distance<=3.5f)
            {
                    return true;
            }
        }
        return false;
    }




































    private void MoveTowards()
    {
        /*    if (Aim() && attack.GetAmmo > 0)
            { return; }
        */
        if (findThePlayer(playerTransform) && attack.GetAmmo > 0)
        { return; }

        if (!canMoveRight)
        {
            Vector3 movep = new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z);
            transform.position = Vector3.MoveTowards(transform.position, movep, speed * Time.deltaTime);
            LookAtTheTarget(movePoints[0].position);
        }
        else
        {
            Vector3 move = new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z);
            transform.position = Vector3.MoveTowards(transform.position, move, speed * Time.deltaTime);
            LookAtTheTarget(movePoints[1].position);
        }
    }
    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position, movePoints[0].position) <= 1f)
        {
            canMoveRight = true;
        }
        else if (Vector3.Distance(transform.position, movePoints[1].position) <= 1f)
        {
            canMoveRight = false;
        }
    }
    private void LookAtTheTarget(Vector3 newTarget)
    {
        Vector3 newlookposition = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newlookposition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }
}