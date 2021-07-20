using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerController : MonoBehaviour
{

    public bool isPlaced = false;
    private Camera cam;
    private RaycastHit hit;
    private Ray ray;

    public float rotationSpeed = 4.0f;
    public GameObject bullet;
    public GameObject target;
    private Enemy_Script _target = null;
    public Transform firePoint;

    public string enemyTag = "Enemy";
    public GameObject[] nodes;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach(GameObject go in gos)
        {
            Vector3 difference = go.transform.position - position;
            float curDistance = difference.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        if(closest != null)
        {
            _target = closest.GetComponent<Enemy_Script>();
        }
        return closest;
    }

    public GameObject FindClosestNode(GameObject[] nodes)
    {
        
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in nodes)
        {
            Vector3 difference = go.transform.position - position;
            float curDistance = difference.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaced)
        {
            target = FindClosestEnemy();
            if (target != null)
            {
                if (!_target.isDead())
                { 
                    float rotationStep = rotationSpeed * Time.deltaTime;
                    Vector3 direction = new Vector3(target.transform.position.x - transform.position.x, 0.0f, 
                                                    target.transform.position.z - transform.position.z);
                    Quaternion rotationToTarget = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

                    if (fireCountdown <= 0f)
                    {
                        Shoot();
                        fireCountdown = 1f / fireRate;
                    }

                    fireCountdown -= Time.deltaTime;
                }
                target = FindClosestEnemy();
                
            }
        }
        else
        {
            nodes = GameObject.FindGameObjectsWithTag("Node");
            GameObject node = FindClosestNode(nodes);

            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if(hit.collider.name == "Plane")
                {
                    transform.position = new Vector3(hit.point.x, 5.0f, hit.point.z);
                    
                }
                
            }
            if (Input.GetMouseButtonDown(0))
            {   
                if (node.transform.childCount == 0)
                {
                    isPlaced = true;
                    transform.position = new Vector3(node.transform.position.x,
                                                     4.5f, node.transform.position.z);
                    gameObject.transform.parent = node.transform;
                }
                
            }

        } 
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
        bulletController bulletCont = bulletGO.GetComponent<bulletController>();

        if(bulletCont != null)
        {
            bulletCont.getTarget(target);
        }
    }
}
