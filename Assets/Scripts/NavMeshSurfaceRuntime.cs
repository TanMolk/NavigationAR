using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class NavMeshSurfaceRuntime : MonoBehaviour
{
    
    [SerializeField]
    NavMeshSurface[] navMeshSurfaces;
    
    public Button rebakeNavmesh;
    private void Start()
    {
        rebakeNavmesh.onClick.AddListener(RebakeNavmesh);
    }

    private void Update()
    {
        
        
    }

    public void RebakeNavmesh()
    {
        for(int i=0; i<navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
        
    }


}
