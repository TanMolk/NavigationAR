using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
public class NavMeshPathDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public LineRenderer lineRenderer;

    public Button Point1;
    public Button Point2;
    public Button Point3;
    public TextMeshProUGUI Distance_meters;
    public Transform startPoint;
    public Transform endPoint1;
    public Transform endPoint2;
    public Transform endPoint3;
    void Start()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        Point1.onClick.AddListener(Destination1);
        Point2.onClick.AddListener(Destination2);
        Point3.onClick.AddListener(Destination3);
        lineRenderer.enabled = true;
    }

    void Update()
    {
        /*float lineLength = 0f;
        for(int i=1; i< lineRenderer.positionCount; i++)
        {
            lineLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i - 1));
        }
        Distance_meters.text = "Distance to Destination is: " + lineLength + " " + "meters";*/

       
    }



    public void Destination1()
    {
       
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(startPoint.position, endPoint1.position, NavMesh.AllAreas, path))
        {
            lineRenderer.positionCount = path.corners.Length;
            for (int i = 0; i < path.corners.Length; i++)
            {
                lineRenderer.SetPosition(i, path.corners[i]);
            }
        }
    
    }

    public void Destination2()
    {

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(startPoint.position, endPoint2.position, NavMesh.AllAreas, path))
        {
            lineRenderer.positionCount = path.corners.Length;
            for (int i = 0; i < path.corners.Length; i++)
            {
                lineRenderer.SetPosition(i, path.corners[i]);
            }
        }

    }
    public void Destination3()
    {

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(startPoint.position, endPoint3.position, NavMesh.AllAreas, path))
        {
            lineRenderer.positionCount = path.corners.Length;
            for (int i = 0; i < path.corners.Length; i++)
            {
                lineRenderer.SetPosition(i, path.corners[i]);
            }
        }

    }


}
