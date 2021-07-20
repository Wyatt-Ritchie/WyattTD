using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject[] Towers;
    public GameObject playableArea;
    public GameObject pathArea;
    public GameObject Node;
    public GameObject waypoint;

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
        //Vector3 cursorPOS = Input.mousePosition;
        GameObject towerGO = (GameObject)Instantiate(Towers[0], playableArea.transform.position, playableArea.transform.rotation);
        //towerGO.transform.position = cursorPOS;

    }
    // Start is called before the first frame update
    void Start()
    {
        Grid = gameObject.GetComponent<Grid>();
        
        
        for(int i = -6; i<6; i++)
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
}
