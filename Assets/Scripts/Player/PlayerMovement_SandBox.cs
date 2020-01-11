using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class PlayerMovement_SandBox : MonoBehaviour
{
    [Header("Camera")]
    public Camera mainCamera;

    [Header("Movement")]
    public new Rigidbody rigidbody;
    private Vector3 inputDirection;
    private Vector3 movement;
    [SerializeField] public float moveSpeed = 4.5f;
    [SerializeField]public float jumpSpeed = 10f;
    [SerializeField]float fallMutiplier = 2.5f;


    [Header("Animation")]
    public Animator playerAnimator;

    // InputActions
    PlayerInputAction_SandBox inputAction;

    // Move
    float movementInput;
    // FireDirection
    Vector2 lookPosition;
    //Jump State
    enum JumpState
    {
        landed,
        jump1,
        jump2
    }
    JumpState jumpState = JumpState.landed;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputAction = new PlayerInputAction_SandBox();
        inputAction.Player.Move.performed += ctx =>
        {
            movementInput = ctx.ReadValue<float>();
        };
        inputAction.Player.FireDirection.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();
        inputAction.Player.jump.performed += ctx => Jump();

    }

    private void Jump()
    {
        if (jumpState == JumpState.jump2) return;

        rigidbody.velocity = new Vector3(0, jumpSpeed, 0);
        jumpState++;
    }

    void FixedUpdate()
    {
        float x = movementInput;

        var targetInput = new Vector3(x, 0, 0);
        inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);//平滑移动

        //Camera Direction
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 desiredDirection = cameraRight * inputDirection.x;

        MoveThePlayer(desiredDirection);
        TurnThePlayer();
        AnimateThePlayer(desiredDirection);
        PullThePlayer();
    }

    private void PullThePlayer()
    {
        if (rigidbody.velocity.y < 0 ||(rigidbody.velocity.y > 0&&!Keyboard.current.spaceKey.isPressed))
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * fallMutiplier * Time.deltaTime;
        }
    }

    private void MoveThePlayer(Vector3 desiredDirection)
    {
        movement.Set(desiredDirection.x, 0f, 0f);

        movement = movement * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);
    }
    private void TurnThePlayer()
    {
        // Old Input system
        Vector2 input = lookPosition;

        // Convert "input" to a Vector3 where the Y axis will be used as the Z axis
        Vector3 lookDirection = new Vector3(input.x, 0, input.y);
        var lookRot = mainCamera.transform.TransformDirection(lookDirection);//将方向向量投影到摄像机坐标系中
        lookRot = Vector3.ProjectOnPlane(lookRot, Vector3.forward);//叫方向向量投影到平面上

        if (lookRot != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(lookRot);
            //rigidbody.MoveRotation(newRotation);//四元数旋转
        }
    }
    private void AnimateThePlayer(Vector3 desiredDirection)
    {
        if (!playerAnimator)
            return;

        Vector3 movement = new Vector3(desiredDirection.x, 0f,0f);
        float forw = Vector3.Dot(movement, transform.forward);

        playerAnimator.SetFloat("Blend", forw);

        //Freeze the animation when character is jumping.
        if(jumpState==JumpState.jump1|| jumpState == JumpState.jump2)
        {
            //todo
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        jumpState = JumpState.landed;
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
