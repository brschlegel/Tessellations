using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSampler : MonoBehaviour
{
    public int numPoints = 100;
    public List<Vector2> points;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numPoints; i++)
        {
            points.Add(new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < points.Count; i++)
        {

            Vector3 pos = new Vector3(points[i].x, transform.position.y + .3f, points[i].y);
            Gizmos.DrawCube(pos, new Vector3(.04f, .04f, .04f));
        }
                            
    }
}
