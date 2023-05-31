using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStriker : MonoBehaviour
{
    [SerializeField] Rigidbody2D strikerRigidbody;  
    [SerializeField] Transform target;              
    [SerializeField] float shootForce = 10f;        
    [SerializeField] Vector2 startingPosition;
    [SerializeField] GameObject playerStrikerRef;
    [SerializeField] GameObject GameManegerRef;

    public bool isShooting = false;                 
    GameManager gameManagerScriptref;

    void OnEnable()
    {
        gameManagerScriptref = GameManegerRef.GetComponent<GameManager>();
        target = GameObject.FindGameObjectWithTag("White").transform;
        transform.position = startingPosition;
        ShootAtTarget();
    }

    void FixedUpdate()
    {
        if (isShooting)
        {
            Vector2 direction = ((Vector2)target.position - strikerRigidbody.position).normalized;
            Vector2 shootForceVector = direction * shootForce;
            strikerRigidbody.AddForce(shootForceVector, ForceMode2D.Impulse);
            isShooting = false;

            Invoke("SetManagerBool", 3f);
        }
    }

    void ShootAtTarget()
    {
        isShooting = true;
    }

    void SetManagerBool()
    {
        gameManagerScriptref.playerTurn = true;
    }
}