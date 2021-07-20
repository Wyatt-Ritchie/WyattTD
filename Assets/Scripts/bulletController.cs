using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    public float movementSpeed = 10.0f;
    public GameObject myTarget = null;
    public float rotationStep = 10.0f;

    private Vector3 lastLoc;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        float movementStep = movementSpeed * Time.deltaTime;
        if(myTarget != null)
        {
            lastLoc = myTarget.transform.position;
            Vector3 direction = myTarget.transform.position - transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);
            gameObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

            transform.position = Vector3.MoveTowards(transform.position, myTarget.transform.position, movementStep);
            if (direction.sqrMagnitude <= 0.3f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Vector3 direction = lastLoc - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, lastLoc, movementStep);
            if (direction.sqrMagnitude <= 0.3f)
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void getTarget(GameObject _target)
    {
        myTarget = _target;
    }


}
