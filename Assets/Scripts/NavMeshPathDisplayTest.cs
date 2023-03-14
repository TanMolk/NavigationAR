using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;
public class NavMeshPathDisplayTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public LineRenderer lineRenderer;
    public Transform StartPoint;
    public Transform EndPoint;
    NavMeshPath path;
    
    float elapsed;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.enabled = true;
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
         NavMeshPath path = new NavMeshPath();
         if (NavMesh.CalculatePath(StartPoint.position, EndPoint.position, NavMesh.AllAreas, path))
         {
             lineRenderer.positionCount = path.corners.Length;
             for (int i = 0; i < path.corners.Length; i++)
             {
                 lineRenderer.SetPosition(i, path.corners[i]);
             }
         }

       /* StartCoroutine(DrawPath(path));
        elapsed += Time.deltaTime;
        if (elapsed > 0.1f) 
        {
            elapsed -= 0.1f;
            NavMesh.CalculatePath(StartPoint.position, EndPoint.position, NavMesh.AllAreas, path);
        }
*/
      
    }

    IEnumerator DrawPath(NavMeshPath path)
    {
        yield return new WaitForEndOfFrame();
        if (path.corners.Length < 2)
            yield break;
        lineRenderer.positionCount = path.corners.Length;

        for(var i=1; i< path.corners.Length; i++)
        {
            Vector3 linePosition = new Vector3(path.corners[i].x, path.corners[i].y, path.corners[i].z);
            lineRenderer.SetPosition(i, linePosition);
        }
    }
}
