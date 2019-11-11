using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{

    // all the cursors
    [SerializeField]
    private Texture2D walkCursor = null;
    [SerializeField]
    private Texture2D attackCursor = null;

    [SerializeField]
    private Texture2D questionCursor = null;


    // layer numbers

    [SerializeField]
    private const int walkableLayerNumber = 9;
    [SerializeField]
    private const int enemyLayerNumber = 10;




    // cursor hotspot
    [SerializeField]
    private Vector2 cursorHotspot = new Vector2(32, 32);

    // Raycaster

   private CameraRaycaster cameraRaycaster;
    // Start is called before the first frame update
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChange; 
    }

    // Update is called once per frame
    void OnLayerChange(int newLayer)
    {
        //print("Over a new layer");
        switch(newLayer)
        {

            case walkableLayerNumber:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case enemyLayerNumber:
            Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                break;
         

            default:
                Cursor.SetCursor(questionCursor, cursorHotspot, CursorMode.Auto);
                return;


        }


        //print(cameraRaycaster.layerHit);
    }
}
