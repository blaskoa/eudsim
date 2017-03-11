using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Duplicate : MonoBehaviour
{
    public void DuplicateComponent()
    {
        if (SelectObject.SelectedObjects.Count != 0)
        {
            //all objects on scene
            GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("ActiveItem");
            GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("ActiveNode");

            //merge two arrays to one
            GameObject[] gameObjects = gameObjects1.Concat(gameObjects2).ToArray();

            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                // Get all game objects and find for the top-left and bottom-right most components
                if (objectSelected.tag.Equals("ActiveItem") || objectSelected.tag.Equals("ActiveNode"))
                {                   
                    // Instantiate new copy GameObject
                    GameObject copy =
                        (GameObject) Instantiate(objectSelected, objectSelected.transform.position, Quaternion.identity);
                    copy.GetComponent<GUICircuitComponent>()
                        .CopyValues(objectSelected.GetComponent<GUICircuitComponent>());                    

                    //add new instance of object to array of active gameobjects
                    GameObject[] gameObjects3 = new GameObject[1]; 
                    gameObjects3[0]= copy;
                    gameObjects = gameObjects.Concat(gameObjects3).ToArray();

                    //get started position of instantiate object
                    Vector3 startPos = new Vector2(
                       copy.transform.position.x,
                       copy.transform.position.y
                    );
                    
                    // Place new copy GameObject
                    bool placed = false;
                    int i = 1;

                    //find colision and drag to clear space on grid
                    while (!placed)
                    {
                        for (int j = i; j >= 0; j--)
                        {             
                            //get all gameobject that intersect with copy of object              
                            ArrayList potentialColliders = new ArrayList();
                            foreach (GameObject go in gameObjects)
                            {
                                //do not colide with yourself
                                if (go != copy)
                                {
                                    //calculating positions
                                    if (Math.Abs((go.transform.position.x + copy.transform.position.x)/2 - go.transform.position.x) <=
                                        1 &&
                                        Math.Abs((go.transform.position.y + copy.transform.position.y) /2 - go.transform.position.y) <=
                                        1)
                                    {
                                        potentialColliders.Add(go);
                                    }
                                }
                            }

                            // Check if the copy GameObject is colliding with any of the existing GameObjects
                            bool touching = false;
                            foreach (GameObject go in potentialColliders)
                            {
                                if (copy.GetComponent<BoxCollider2D>()
                                    .bounds.Intersects(go.GetComponent<BoxCollider2D>().bounds))
                                {
                                    touching = true;
                                    break;
                                }
                            }

                            // Stop the placement algorithm
                            if (!touching)
                            {
                                placed = true;
                                break;
                            }

                            //transfprm position of new copy object
                            copy.transform.position = startPos + new Vector3(j, j - i, 0f);
                        }
                        i++;
                    }
                }
            }
        }
    }
}