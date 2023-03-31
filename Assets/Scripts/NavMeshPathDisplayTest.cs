using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;
using UnityEngine.UI;
public class NavMeshPathDisplayTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public LineRenderer lineRenderer;
    public Transform startPoint;
    Transform endPoint;
    NavMeshPath path;

    public Button roomButton;
    public Button bathroomButton;
    public Button entranceButton;
    public Transform roomLoc;
    public Transform bathroomLoc;
    public Transform entranceLoc;
    bool isRoom;
    bool isBathroom;
    bool isEntrance;
    
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
       
        
        roomButton.onClick.AddListener(OnClickRoom);
        bathroomButton.onClick.AddListener(OnClickBathroom);
        entranceButton.onClick.AddListener(OnClickEntrance);
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshPath path = new NavMeshPath();
        if (isRoom)
        {
            
            if (NavMesh.CalculatePath(startPoint.position, roomLoc.position, NavMesh.AllAreas, path))
            {
                lineRenderer.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    lineRenderer.SetPosition(i, path.corners[i]);
                }
            }

        }
        if (isBathroom)
        {
            if (NavMesh.CalculatePath(startPoint.position, bathroomLoc.position, NavMesh.AllAreas, path))
            {
                lineRenderer.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    lineRenderer.SetPosition(i, path.corners[i]);
                }
            }
        }

        if (isEntrance)
        {
            if (NavMesh.CalculatePath(startPoint.position, entranceLoc.position, NavMesh.AllAreas, path))
            {
                lineRenderer.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    lineRenderer.SetPosition(i, path.corners[i]);
                }
            }
        }
      

      
    }

    public void OnClickRoom()
    {
        isRoom = true;
        isBathroom = false;
        isEntrance = false;
       
   

    }
    public void OnClickBathroom()
    {
        isRoom = false;
        isBathroom = true;
        isEntrance = false;
        

    }
    public void OnClickEntrance()
    {
        isRoom = false;
        isBathroom = false;
        isEntrance = true;
        
    }

    
}
