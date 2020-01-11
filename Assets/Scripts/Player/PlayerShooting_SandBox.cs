
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerShooting_SandBox : MonoBehaviour
{
    PlayerInputAction_SandBox inputAction;
    SlowTapInteraction slowTap = new SlowTapInteraction();
    TapInteraction tap = new TapInteraction();
    float chargingTime,currentTime=0;
    bool isStartCharging = false;

    void Awake()
    {
        InputInitialization();
    }

    private void Update()
    {
        if (isStartCharging)
        {
            chargingTime = slowTap.duration - tap.duration;

            currentTime += Time.deltaTime;
            if (chargingTime < currentTime)
            {
                Debug.Log("Charging Complete");
                isStartCharging = false;
                currentTime = 0;
            }
        }
    }
    /// <summary>
    /// 初始化设计input，可蓄力
    /// </summary>
    private void InputInitialization()
    {
        inputAction = new PlayerInputAction_SandBox();

        inputAction.Player.Shoot.started +=
        context =>
        {
            if (context.interaction is TapInteraction)
                tap = (TapInteraction)context.interaction;
            if(context.interaction is SlowTapInteraction)
            {
                slowTap = (SlowTapInteraction)context.interaction;
                Charging();
            }
        };
        inputAction.Player.Shoot.performed +=
        context =>
        {
            if (context.interaction is SlowTapInteraction)
                FullChargedFire();
            else
                Fire();
        };

        inputAction.Player.Shoot.canceled +=
        context =>
        {
            if (context.interaction is SlowTapInteraction)
                ChargedFire();
        };
    }

    private void ChargedFire()
    {
        Debug.Log("ChargedFire");
        currentTime = 0;
        isStartCharging = false;
    }

    private void Fire()
    {
        Debug.Log("Fire");
    }

    private void FullChargedFire()
    {
        Debug.Log("FullChargedFire");
    }

    private void Charging()
    {
        Debug.Log("Start Charging");
        isStartCharging = true;
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
