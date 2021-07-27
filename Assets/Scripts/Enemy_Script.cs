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
    public bool isDead()
    {
        return dead;
    }

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

    private void OnTriggerEnter(Collider collision)
    {
        bulletController bcSC = collision.GetComponent<bulletController>();
        if(collision.tag == "Projectile" && bcSC.getTarget() == gameObject)
        {
            health -= bcSC.getDamage();
        }
         
    }
    public IEnumerator callDeathAnim()
    {
        if (!paid)
        {
            pSC.updateMoney(10);
            paid = true;
        }
        
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
