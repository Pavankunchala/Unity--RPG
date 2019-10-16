using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };
    [SerializeField]
    private float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit rayCastHit;
    public RaycastHit hit
    {
        get { return rayCastHit; }
    }

    Layer layerHit;
    public Layer currentLayerHit
    {
        get { return layerHit; }
    }


    public delegate void OnLayerChange(); // Declaring a new  delegate
    public OnLayerChange layerChangeObservers;


    
        


    void Start() 
    {
        viewCamera = Camera.main;

       
        
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                rayCastHit = hit.Value;
                if(layerHit != layer) // if layer has changed
                {
                    layerHit = layer;
                    layerChangeObservers(); // call delegate
                }
                layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        rayCastHit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
