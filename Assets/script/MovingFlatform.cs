using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatform : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform posA, posB;
    public float Speed;
    Vector3 targetPos;
    void Start()
    {
        targetPos = posB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,posA.position) <0.05f)
        {
            targetPos = posB.position;
        }
        if(Vector2.Distance(transform.position,posB.position) <0.05f)
        {
            targetPos = posA.position;

        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed *Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
