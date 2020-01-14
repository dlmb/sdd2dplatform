using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Movement")]

    [Tooltip("玩家走路的速度")] [SerializeField] private float walkSpeed = 4.5f;
    [Tooltip("玩家跑动速度")] [SerializeField] private float runSpeed = 9f;
    [Tooltip("玩家跑动速度")] [SerializeField] private float squatSpeed = 3f;
    [Tooltip("玩家移动时的加速度乘数，默认为1")] [SerializeField] private float moveAccMutiplier = 1f;
    [SerializeField] private float jumpHeight = 10f;
    [Tooltip("玩家二段跳跳跃高度")] [SerializeField] private float jump2Height = 6f;
    [Tooltip("玩家跳跃高度")] [SerializeField] private float jumpSpeed, jump2Speed;
    [Tooltip("玩家掉落时的加速度乘数")] [SerializeField] float fallMutiplier = 2.5f;
    [Tooltip("玩家掉落多长时间后无法跳跃")] [SerializeField] float dropDeadTimeCount = 2f;
    [Tooltip("玩家蹲下时角色碰撞体的高度")] [SerializeField] private float squatColliderHeight = 0.89f;
    [Tooltip("玩家角色碰撞体的高度")] [SerializeField] private float normalColliderHeight = 1.78f;


    [Header("Animation")]
    public Animator playerAnimator;
    PlayerInputAction inputAction;

    private float inputDirection;
    private Vector3 movement;
    float movementInputX, movementInputY;
    // FireDirection
    Vector2 lookPosition;

    //movement State
    enum MovementState
    {
        slide,//*滑动
        jump,//浮空，可2段跳
        jump2,
        landed,//落地
        hang,//悬挂（梯子，*绳子）
        stair//楼梯
    }
    MovementState movementState = MovementState.landed;
    enum BasicMovementState
    {
        walk,
        run,
        squat
    }
    BasicMovementState basicState = BasicMovementState.walk;

    void Awake()
    {
        iniValue();
        iniInputAction();
    }

    private void iniValue()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //计算跳跃速度
        jumpSpeed = Mathf.Sqrt(2 * (-Physics.gravity.y) * jumpHeight);
        jump2Speed = Mathf.Sqrt(2 * (-Physics.gravity.y) * jump2Height);
    }
    #region Movement
    private void iniInputAction()
    {
        inputAction = new PlayerInputAction();
        inputAction.Player.Move.performed += ctx => movementInputX = ctx.ReadValue<float>();
        inputAction.Player.FireDirection.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();
        inputAction.Player.jump.performed += ctx => Jump();
        inputAction.Player.squat.performed += ctx => Squat(ctx);
        inputAction.Player.run.performed += ctx => Run(ctx);

    }

    private void Squat(InputAction.CallbackContext ctx)
    {
        var button = (ButtonControl)ctx.control;
        
        if (button.wasPressedThisFrame)
        {
            basicState = BasicMovementState.squat;

            capsuleCollider.center -=
           new Vector3(0,
           (capsuleCollider.height - squatColliderHeight) / 2,
           0);
            capsuleCollider.height = squatColliderHeight;
            movementInputY = -1;
        }
        else if (button.wasReleasedThisFrame)
        {
            basicState = BasicMovementState.walk;

            capsuleCollider.height = normalColliderHeight;
            capsuleCollider.center = new Vector3(0, capsuleCollider.height / 2, 0);

            movementInputY = 0;
        }
       
    }

    private void Run(InputAction.CallbackContext ctx)
    {
        var button = (ButtonControl)ctx.control;
        if (button.wasPressedThisFrame)
        {
            basicState = BasicMovementState.run;
            movementInputY = 1;
        }
        else if (button.wasReleasedThisFrame)
        {
            basicState = BasicMovementState.walk;
            movementInputY = 0;
        }
    }

    private void Jump()
    {
        basicState = BasicMovementState.walk;
        switch (movementState)
        {
            case MovementState.landed:
                rigidbody.velocity = new Vector3(0, jumpSpeed, 0);
                movementState = MovementState.jump;
                break;
            case MovementState.jump:
                rigidbody.velocity = new Vector3(0, jump2Speed, 0);
                movementState = MovementState.jump2;
                break;
            case MovementState.jump2:
                return;
        }
    }
    #endregion
    void FixedUpdate()
    {
        var targetInput = new Vector3(movementInputX, 0, 0);
        //平滑移动
        inputDirection = Mathf.Lerp(inputDirection, movementInputX, Time.deltaTime * 10f * moveAccMutiplier);
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
    private void MoveThePlayer(Vector3 desiredDirection)
    {
        movement.Set(desiredDirection.x, 0f, 0f);
        if(movementState == MovementState.jump)
                movement = movement * walkSpeed * Time.deltaTime;
        else switch (basicState)
        {
            case BasicMovementState.walk:
                movement = movement * walkSpeed * Time.deltaTime;
                break;
            case BasicMovementState.run:
                movement = movement * runSpeed * Time.deltaTime;
                break;
            case BasicMovementState.squat:
                movement = movement * squatSpeed * Time.deltaTime;
                break;

        }
        rigidbody.MovePosition(transform.position + movement);
    }

    //Todo: 鼠标
    private void TurnThePlayer()
    {
        // Old Input system
        Vector2 input = lookPosition;

        Vector3 lookDirection = new Vector3(input.x, 0, input.y);
        var lookRot = mainCamera.transform.TransformDirection(lookDirection);//将方向向量投影到摄像机坐标系中
        lookRot = Vector3.ProjectOnPlane(lookRot, Vector3.forward);//叫方向向量投影到平面上

        if (lookRot != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(lookRot);
            //rigidbody.MoveRotation(newRotation);//四元数旋转
        }
    }
    private void PullThePlayer()
    {
        if (rigidbody.velocity.y < 0 ||
            (rigidbody.velocity.y > 0 && !Keyboard.current.spaceKey.isPressed))
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * fallMutiplier * Time.deltaTime;
        }
    }

    private void AnimateThePlayer(Vector3 desiredDirection)
    {
        if (!playerAnimator)
            return;

        Vector3 movement = new Vector3(desiredDirection.x, 0f, 0f);
        float forw = Vector3.Dot(movement, transform.forward);

        playerAnimator.SetFloat("bmpX", forw);
        playerAnimator.SetFloat("bmpY", movementInputY);

        //Freeze the animation when character is jumping.
        if (movementState == MovementState.jump || movementState == MovementState.jump2)
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

class MovementInfo
{
    public bool isSpeedHOverride = true,
                       isSpeedVOverride=false;
    float speedH, speedV;
    public float SpeedH { get => speedH; set => speedH = value; }
    public float SpeedV { get => speedV; set => speedV = value; }
    public enum MovementState
        {
            basic,
            wall,
            ladder,
            jump,
            airJump,
        }
        MovementState state = MovementState.basic;
    public enum BasicMovementState
    {
        idle,
        walk,
        run,
        squat
    }
    BasicMovementState basicState = BasicMovementState.idle;

    Stack<BasicMovementState> BasicStateStack = new Stack<BasicMovementState>();

    void ChangeState(BasicMovementState basicState)
    {
        switch (basicState)
        {
            case BasicMovementState.idle:
                break;
            case BasicMovementState.walk:
                break;
            case BasicMovementState.run:
                break;
            case BasicMovementState.squat:
                break;
        }
    }
    void ChangeState(MovementState State)
    {

    }
}
