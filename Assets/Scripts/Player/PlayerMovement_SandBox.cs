using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;


public class PlayerMovement_SandBox : MonoBehaviour
{
    public new Rigidbody rigidbody;
    private new CapsuleCollider capsuleCollider;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Movement")]

    [Tooltip("玩家走路的速度")] [SerializeField] private float walkSpeed = 4.5f;
    [Tooltip("玩家跑动速度")] [SerializeField] private float runSpeed = 9f;
    [Tooltip("玩家移动时的加速度乘数，默认为1")] [SerializeField] private float moveAccMutiplier = 1f;
    [SerializeField] private float jumpHeight=10f;
    [Tooltip("玩家二段跳跳跃高度")] [SerializeField] private float jump2Height=6f;
    [Tooltip("玩家跳跃高度")] [SerializeField] private float jumpSpeed,jump2Speed;
    [Tooltip("玩家掉落时的加速度乘数")] [SerializeField]float fallMutiplier = 2.5f;
    [Tooltip("玩家掉落多长时间后无法跳跃")] [SerializeField]float dropDeadTimeCount = 2f;


    [Header("Animation")]
    public Animator playerAnimator;
    PlayerInputAction_SandBox inputAction;

    private float inputDirection;
    private Vector3 movement;
    float movementInput;
    // FireDirection
    Vector2 lookPosition;

    //movement State
    enum MovementState { 
        idle,//空闲
        walk,//走
        run,//跑
        squat,//蹲
        slide,//*滑动
        jump,//浮空，可2段跳
        jump2,//跳，不可2段跳
        landed,//落地
        hang,//悬挂（梯子，*绳子）
        stair//楼梯
    }
    MovementState movementState = MovementState.idle;

    public CapsuleCollider CapsuleCollider { get => capsuleCollider; set => capsuleCollider = value; }

    void Awake()
    {
        iniValue();
        iniInputAction();
    }

    private void iniValue()
    {
        rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        //计算跳跃速度
        jumpSpeed = Mathf.Sqrt(2 * (-Physics.gravity.y) * jumpHeight);
        jump2Speed = Mathf.Sqrt(2 * (-Physics.gravity.y) * jump2Height);
    }

    private void iniInputAction()
    {
        inputAction = new PlayerInputAction_SandBox();
        inputAction.Player.Move.performed += ctx => movementInput = ctx.ReadValue<float>();
        inputAction.Player.FireDirection.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();
        inputAction.Player.jump.performed += ctx => Jump();
        inputAction.Player.run.performed += ctx =>
        {
            Run(ctx);
        };
    }

    private void Run(InputAction.CallbackContext ctx)
    {
        var button = (ButtonControl)ctx.control;
        if (button.wasPressedThisFrame 
            && movementState == MovementState.walk)
        {
            movementState = MovementState.run;
        }
        else if (button.wasReleasedThisFrame 
            && movementState == MovementState.run)
        {
            movementState = MovementState.walk;
        }
    }

    private void Jump()
    {
        switch (movementState)
        {
            case MovementState.landed:
                rigidbody.velocity = new Vector3(0, jumpSpeed, 0);
                movementState=MovementState.jump;
                break;
            case MovementState.jump:
                rigidbody.velocity = new Vector3(0, jump2Speed, 0);
                movementState = MovementState.jump2;
                break;
            case MovementState.jump2:
                return;
        }
    }

    void FixedUpdate()
    {

        float x = movementInput;
        var targetInput = new Vector3(x, 0, 0);
        inputDirection = Mathf.Lerp(inputDirection, x, Time.deltaTime * 10f * moveAccMutiplier);
        //Camera Direction
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 desiredDirection = cameraRight * inputDirection;

        MoveThePlayer(desiredDirection);
        TurnThePlayer();
        PullThePlayer();
        AnimateThePlayer(desiredDirection);
    }

    private void PullThePlayer()
    {
        if (rigidbody.velocity.y < 0 ||
            (rigidbody.velocity.y > 0&&!Keyboard.current.spaceKey.isPressed))
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * fallMutiplier * Time.deltaTime;
        }
    }
    private void MoveThePlayer(Vector3 desiredDirection)
    {
        movement.Set(desiredDirection.x, 0f, 0f);
        switch (movementState)
        {
            case MovementState.jump:
                movement = movement * walkSpeed * Time.deltaTime;
                break;
            case MovementState.walk: 
                movement = movement * walkSpeed * Time.deltaTime;
                break;
            case MovementState.run:
                movement = movement * runSpeed * Time.deltaTime;
                break;
        }

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
        if(movementState==MovementState.jump|| movementState == MovementState.jump2)
        {
            //todo
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        movementState = MovementState.landed;
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
