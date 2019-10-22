﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); 
        
        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                //Attack the target
                FaceTarget();
            }
        }
    
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; // get the direction to the target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // rotation to point to the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4.0f); // apply rotation to the character with interpolation for the smoothness
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); 
    }
}
