using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{

    [Header("Attributes")]
    public int health = 10;
    public Animator anim;
    public bool dead = false;

    private gameManager pSC;
    private bool paid = false;

    // Start is called before the first frame update
    void Start()
    {
        pSC = FindObjectOfType<gameManager>();
        anim = GetComponent<Animator>();
        anim.SetBool("RUN", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            anim.SetBool("RUN", false);
            anim.SetBool("Death", true);
            
            StartCoroutine(callDeathAnim());
        }
    }

    // function for updating damage after collision with bullet
    private void OnTriggerEnter(Collider collision)
    {
        bulletController bcSC = collision.GetComponent<bulletController>();
        if(collision.tag == "Projectile" && bcSC.getTarget() == gameObject)
        {
            health -= bcSC.getDamage();
        }
         
    }

    // this coroutine increases the amount of money in the game manager and starts the death animation
    public IEnumerator callDeathAnim()
    {
        if (!paid)
        {
            pSC.updateMoney(10);
            paid = true;
            GameObject powerUp = spawnPowerUp(pSC.getPowerups(), pSC.getWeights());
            if(powerUp != null)
            {
                GameObject p = (GameObject)Instantiate(powerUp, this.transform.position, this.transform.rotation);
            }
            
        }
        
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    // returns the boolean dead
    public bool isDead()
    {
        return dead;
    }

    private GameObject spawnPowerUp(GameObject[] powerups, int[] weights)
    { 
        
        int roll = (int)(Random.Range(0f, 100f));
        for(int i=0; i<weights.Length; i++)
        {
            if(roll <= weights[0])
            {
                break;
            }
            else
            {
                return powerups[0];
            }
        }

        return null;
    }
}
