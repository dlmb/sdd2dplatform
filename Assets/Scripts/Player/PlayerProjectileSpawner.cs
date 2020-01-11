using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileSpawner : MonoBehaviour {
    
	[Header("Spawner Settings")]
	public GameObject projectilePrefab;
	public Transform spawnPoint;

	public float spawnRate;
	private float timer;


	[Header("Particles")]
	public ParticleSystem spawnParticles;


	[Header("Audio")]
	public AudioSource spawnAudioSource;

    PlayerInputActions inputAction;

    Vector2 lookPosition;

    private void Awake()
    {
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.FireDirection.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    void Update()
	{

		timer += Time.deltaTime;

        if(lookPosition.magnitude > 0.1f)
        {
            Shoot();
        }

	}
	

	void Shoot()
	{
        if (timer < spawnRate) { return; }
        timer = 0f;
		Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

		if(spawnParticles)
		{
			spawnParticles.Play();
		}

		if(spawnAudioSource)
		{
			spawnAudioSource.Play();
		}

	}

}
