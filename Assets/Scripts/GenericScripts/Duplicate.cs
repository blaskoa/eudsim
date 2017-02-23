using System;
using UnityEngine;
using System.Collections;

public class Duplicate : MonoBehaviour
{
    public void duplicate()
    {
        if (SelectObject.SelectedObject != null && SelectObject.SelectedObject.tag.Equals("ActiveItem"))
        {
            // Get all game objects and find for the top-left and bottom-right most components
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ActiveItem");
            
            Vector2 startPos = new Vector2(
                SelectObject.SelectedObject.transform.position.x,
                SelectObject.SelectedObject.transform.position.y
                );

            // Array List of potential colliders: GameObjects that are placed to the right/bottom of the coppying GameObject
            ArrayList potentialColliders = new ArrayList();
            foreach (GameObject go in gameObjects)
            {
                if (go.transform.position.x >= startPos.x && go.transform.position.y <= startPos.y)
                {
                    potentialColliders.Add(go);
                }
            }

            // Instantiate new copy GameObject
            GameObject copy = (GameObject)Instantiate(SelectObject.SelectedObject, startPos, Quaternion.identity);

            // Place new copy GameObject
            Boolean placed = false;
            int i = 1;
            while (!placed)
            {
                for (int j = i; j >= 0; j--)
                {
                    // FIXME: Constants to be changed for dynamically calculated values (larger area for multiselect)
                    Vector2 position = startPos + new Vector2(j * 1.5f, j * 1.5f - i * 1.5f);
                    copy.transform.position = new Vector3(position.x, position.y, 0);

                    // Check if the copy GameObject is colliding with any of the existing GameObjects
                    Boolean touching = false;
                    foreach (GameObject go in potentialColliders)
                    {
                        if (copy.GetComponent<BoxCollider2D>().bounds.Intersects(go.GetComponent<BoxCollider2D>().bounds))
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
                }
                i++;
            }
        }
    }
}
