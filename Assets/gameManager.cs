using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [Header("Manager Game Objects")]
    public GameObject[] Towers;
    public GameObject playableArea;
    public GameObject pathArea;
    public GameObject Node;
    public GameObject waypoint;
    public GameObject[] PowerUps;

    [Header("Manager Attributes")]
    public int money = 100;
    public int lives = 20;
    public int towerPrice = 50;
    private int[] weights = {95, 5};

    private int[,] enemyPath = new int[12, 12] { { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
                                                 { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
                                                 { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
                                                 { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
                                                 { 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
                                                 { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                                                 { 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0 },
                                                 { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 },
                                                 { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 },
                                                 { 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0 },
                                                 { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                 { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,} };
    private Grid Grid; 

    public void spawnBulletTower()
    {
        if (money < towerPrice) return;
        GameObject towerGO = (GameObject)Instantiate(Towers[0], playableArea.transform.position, playableArea.transform.rotation);
        

    }
    // Start is called before the first frame update
    void Start()
    {
        Grid = gameObject.GetComponent<Grid>();
        GameObject mText = GameObject.Find("moneyText");
        mText.GetComponent<UnityEngine.UI.Text>().text = "Gold: " + money.ToString();
        GameObject lText = GameObject.Find("livesText");
        lText.GetComponent<UnityEngine.UI.Text>().text = "Lives: " + lives.ToString();


        for (int i = -6; i<6; i++)
        {
            for(int j = -6; j<6; j++)
            {
                Vector3Int place = new Vector3Int(i, j, -1);
                GameObject nodeGO = (GameObject)Instantiate(Node);
                nodeGO.transform.position = Grid.GetCellCenterWorld(place);
                if (enemyPath[i+6,j+6] != 0)
                {
                    GameObject pathGO = (GameObject)Instantiate(pathArea);
                    pathGO.transform.position = Grid.GetCellCenterWorld(place);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getLives()
    {
        return lives;
    }
    public int getMoney()
    {
        return money;
    }
    public void reduceLives(int damage)
    {
        lives -= damage;
        GameObject lText = GameObject.Find("livesText");
        lText.GetComponent<UnityEngine.UI.Text>().text = "Lives: " + lives.ToString();
    }
    public void updateMoney(int delta)
    {
        money += delta;
        GameObject mText = GameObject.Find("moneyText");
        mText.GetComponent<UnityEngine.UI.Text>().text = "Gold: " + money.ToString();
    }
}
