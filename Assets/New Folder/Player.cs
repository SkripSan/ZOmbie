using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public float moveSpeed;
    private Cursor cursor;
    private Shoot shoot;
    public Transform gunBarrel;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cursor = FindObjectOfType<Cursor>();
        navMeshAgent.updateRotation = false;

        shoot = FindObjectOfType<Shoot>();
    }
    
    void Update()
    {
        //Ходим
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
            dir.z = 1.0f;
        if (Input.GetKey(KeyCode.RightArrow))
            dir.z = -1.0f;
        if (Input.GetKey(KeyCode.UpArrow))
            dir.x = 1.0f;
        if (Input.GetKey(KeyCode.DownArrow))
            dir.x = -1.0f;
        navMeshAgent.velocity = dir.normalized * moveSpeed;
        Vector3 forward = cursor.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));
        if (Input.GetMouseButton(0))
        {
            var from = gunBarrel.position;
            var target = cursor.transform.position;
            var to = new Vector3(target.x, from.y, target.z);
            var direction = (to - from).normalized;
            RaycastHit hit;
            if (Physics.Raycast(from, to - from, out hit, 100))
                to = new Vector3(hit.point.x, from.y, hit.point.z);
            else
            {
                to = from + direction * 100;
            }
            shoot.Show(from,to);
            if (hit.transform != null)
            {
                var zombie = hit.transform.GetComponent<Zombie>();
                if (zombie != null)
                    zombie.Kill();
            }
        }

    }
}
