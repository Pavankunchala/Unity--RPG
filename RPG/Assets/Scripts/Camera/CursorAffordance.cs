using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour
{

    // all the cursors
    [SerializeField]
    private Texture2D walkCursor = null;
    [SerializeField]
    private Texture2D attackCursor = null;

    [SerializeField]
    private Texture2D questionCursor = null;

   


    // cursor hotspot
    [SerializeField]
    private Vector2 cursorHotspot = new Vector2(96, 96);

    // Raycaster

   private CameraRaycaster cameraRaycaster;
    // Start is called before the first frame update
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(cameraRaycaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(questionCursor, cursorHotspot, CursorMode.Auto);
                break;

            default:
                print("Don't know");
                return;


        }


        //print(cameraRaycaster.layerHit);
    }
}
