using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeCamera : MonoBehaviour
{
    public float activationDistance = 5f; // Adjust this value to change the distance at which the text appears

    private Transform player;

    TextMeshPro textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        player = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveOnDistacnce();
    }

    private void FaceCamera()
    {
        
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(directionToCamera);
        
    }

    private void ActiveOnDistacnce()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= activationDistance)
        {

            textMesh.enabled = true;

            transform.rotation = Quaternion.LookRotation(transform.position - player.position);
        }
        else
        {
            textMesh.enabled = false;
        }
    }
}
