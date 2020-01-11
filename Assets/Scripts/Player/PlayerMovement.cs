using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    [Header("Camera")]
    public Camera mainCamera;

    [Header("Movement")]
    public Rigidbody playerRigidbody;
    public float speed = 4.5f;
    private Vector3 inputDirection;
    private Vector3 movement;


    //Rotation

    private Plane playerMovementPlane;

    private RaycastHit floorRaycastHit;

    private Vector3 playerToMouse;


    [Header("Animation")]
    public Animator playerAnimator;

    // InputActions
    PlayerInputActions inputAction;

    // Move
    Vector2 movementInput;
    // FireDirection
    Vector2 lookPosition;


    void Awake() {
        //CreatePlayerMovementPlane();
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.PlayerControls.FireDirection.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();
    }

    void CreatePlayerMovementPlane() {
        playerMovementPlane = new Plane(transform.up, transform.position + transform.up);
    }

    void FixedUpdate() {

        //Old InputSystem Input
        float h = movementInput.x;
        float v = movementInput.y;

        var targetInput = new Vector3(h, 0, v);
        inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);//平滑移动

        //Camera Direction
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        //Try not to use var for roadshows or learning code
        Vector3 desiredDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

        //Why not just pass the vector instead of breaking it up only to remake it on the other side?
        MoveThePlayer(desiredDirection);
        TurnThePlayer();
        AnimateThePlayer(desiredDirection);
    }

    void MoveThePlayer(Vector3 desiredDirection) {
        movement.Set(desiredDirection.x, 0f, desiredDirection.z);

        movement = movement * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);

    }

    void TurnThePlayer() {

        // Old Input system
        Vector2 input = lookPosition;

        // Convert "input" to a Vector3 where the Y axis will be used as the Z axis
        Vector3 lookDirection = new Vector3(input.x, 0, input.y);
        var lookRot = mainCamera.transform.TransformDirection(lookDirection);//将方向向量投影到摄像机坐标系中
        lookRot = Vector3.ProjectOnPlane(lookRot, Vector3.up);//叫方向向量投影到平面上
        
        if (lookRot != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(lookRot);
            playerRigidbody.MoveRotation(newRotation);//四元数旋转
        }
    }

    Vector3 PlaneRayIntersection(Plane plane, Ray ray) {
        float dist = 0.0f;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }

    Vector3 ScreenPointToWorldPointOnPlane(Vector3 screenPoint, Plane plane, Camera camera) {
        Ray ray = camera.ScreenPointToRay(screenPoint);
        return PlaneRayIntersection(plane, ray);
    }


    void AnimateThePlayer(Vector3 desiredDirection) {
        if (!playerAnimator)
            return;

        Vector3 movement = new Vector3(desiredDirection.x, 0f, desiredDirection.z);
        float forw = Vector3.Dot(movement, transform.forward);
        float stra = Vector3.Dot(movement, transform.right);

        playerAnimator.SetFloat("Forward", forw);
        playerAnimator.SetFloat("Strafe", stra);
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }
}
