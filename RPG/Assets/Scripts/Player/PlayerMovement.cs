using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkMoveStopRadius = .2f;
    [SerializeField]
    private float attackMoveStopRadius = .5f;

    // to check wheter he is in direct mode or not
    private bool isIndirectMode = false;

    AICharacterControl aICharacterControl =null;


    ThirdPersonCharacter m_Character = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster  = null; 
    Vector3 currentDestination,clickPoint;

    [SerializeField]
    private const int walkableLayerNumber = 9;
    [SerializeField]
    private const int enemyLayerNumber = 10;

    private GameObject walkTarget = null;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
        aICharacterControl = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("WalkTarget");


        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
    }

    private void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case enemyLayerNumber:
                // navigate to the enemy
                GameObject enemy = raycastHit.collider.gameObject;
                aICharacterControl.SetTarget(enemy.transform);
                break;
            case walkableLayerNumber:
                // navigae to the ground
                walkTarget.transform.position = raycastHit.point;
                aICharacterControl.SetTarget(walkTarget.transform);
                break;

            default:
                Debug.LogWarning("Don't know how to handlem it yet");

                return;


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



  


}// Player Movement




