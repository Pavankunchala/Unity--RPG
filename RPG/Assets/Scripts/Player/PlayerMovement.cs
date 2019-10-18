using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkMoveStopRadius = .2f;
    [SerializeField]
    private float attackMoveStopRadius = .5f;

    // to check wheter he is in direct mode or not
    private bool isIndirectMode = false;


    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster; 
    Vector3 currentDestination,clickPoint;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {

        if(Input.GetKeyDown(KeyCode.G))
        {
            isIndirectMode = !isIndirectMode; // toggle mode

            currentDestination = transform.position; // clear the current  bug
        }

        if(isIndirectMode)
        {
            DirectMovement();
        }
        else 
        {
            MouseMovement();// Mouse moment
        }
       

    }

    private void DirectMovement()
    {

        print("DirectMovement");
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

      Vector3  cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
       Vector3  movement = v * cameraForward + h * Camera.main.transform.right;

        m_Character.Move(movement, false, false);
    }

    private void MouseMovement()
    {

        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;
            print("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString());
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    //currentDestination = clickPoint;
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);

                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
                    print("Moving towards an enemy");
                    break;

                default:
                    print("Have no idea whaty to do");
                    return;
            }


        }
        WalkToDestination();
    }// Mouse movemnt

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;

        if (playerToClickPoint.magnitude >= 0)
        {
            m_Character.Move(playerToClickPoint, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }

    private void OnDrawGizmos()
    { 
        print("Draw Gizmo");
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination, .1f);
        Gizmos.DrawSphere(clickPoint, .15f);
       

        // Attack Sphere
        Gizmos.color = new Color(255f, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
       

    }

    Vector3 ShortDestination(Vector3 destination ,float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }


}// Player Movement




