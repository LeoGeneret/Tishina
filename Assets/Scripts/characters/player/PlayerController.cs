using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float gravity = -12;
	public float jumpHeight = 1;
	[Range(0, 1)]
	public float airControlPercent;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	public Animator animator;
	Camera cam;
	ThirdPersonCamera camController;
	Transform cameraT;
	CharacterController controller;


	float startFov;
	float aimFov = 40f;
	public float aimSmoothTime = 1f;
	public bool recordingStatus = false;

	private Interactable _interactable;

	// TODO on interactable side
	public float ammo;
	public float maxAmmo = 10;

	// GUN
	[Range(0f, 1.5f)]
	public float fireRate = 0.3f;
	[Range(1, 10)]
	public int damage = 1;
	private float timer;

	void Start()
	{
		// animator = GetComponent<Animator>();
		cam = Camera.main;
		camController = cam.GetComponent<ThirdPersonCamera>();
		cameraT = Camera.main.transform;
		startFov = Camera.main.fieldOfView;
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		// input
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;
		//bool running = Input.GetKey(KeyCode.LeftShift);
		// FORCE RUNING MODE
		bool running = true;
		bool aiming = Input.GetMouseButton(1);

		// GUN TIMER
		timer += Time.deltaTime;

		Zoom(aiming);

		if (!recordingStatus)
        {
			if (camController.getCamLockStatus())
            {
				camController.setCamLockStatus(false);
			}

			Move(inputDir, running, aiming);

			if (Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}

			// animator
			float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
			animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		} else
        {
			if (!camController.getCamLockStatus())
			{
				camController.setCamLockStatus(true);
			}

			float animationSpeedPercent = 0f;
			animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		}
	}

	public void setRecording(bool status)
    {
		recordingStatus = status;

		animator.SetBool("recording", recordingStatus);
	}

	void Move(Vector2 inputDir, bool running, bool aiming)
	{
		if (inputDir != Vector2.zero || aiming)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}

		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		controller.Move(velocity * Time.deltaTime);
		//currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded)
		{
			velocityY = 0;
		}

	}

	void Zoom(bool aiming)
    {
		animator.SetBool("aiming", aiming);

		if (_interactable != null)
		{
			_interactable.aimingStatus(false);
			_interactable = null;
		}

		if (aiming)
        {
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, aimFov, aimSmoothTime * Time.deltaTime);

			bool shoot = Input.GetMouseButton(0);
			bool shootPress = Input.GetMouseButtonDown(0);
			bool shootPressUp = Input.GetMouseButtonUp(0);

			Ray ray = cam.ViewportPointToRay(Vector3.one * 0.5f);

			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, 600))
			{
				var interactable = hitInfo.collider.GetComponent<Interactable>();

				if (interactable != null)
                {
					_interactable = interactable;
					interactable.aimingStatus(true);
				}

				var enemy = hitInfo.collider.GetComponent<Enemy>();

				if (enemy != null)
				{
					if (shootPress)
                    {
						int vacumHash = Animator.StringToHash("vacum");
						animator.SetTrigger(vacumHash);
					}


					if (shoot)
					{
						animator.SetBool("vacuming", true);

						if (timer >= fireRate)
						{
							timer = 0f;
							FireGun(enemy);
						}
					}
					else
					{
						animator.SetBool("vacuming", false);
					}
				} else
                {
					animator.SetBool("vacuming", false);
				}

				if (shootPressUp)
                {
					animator.SetBool("vacuming", false);
				}
			} else
            {
				animator.SetBool("vacuming", false);
			}
		} else
        {
			animator.SetBool("vacuming", false);
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, startFov, aimSmoothTime * Time.deltaTime);
		}
    }

	public void Shoot()
    {
		int shootHash = Animator.StringToHash("shoot");
		animator.SetTrigger(shootHash);
	}

	private void FireGun(Enemy enemy)
    {
		var health = enemy.GetComponent<Health>();

		health.TakeDamage(damage);

		if (health.isDead())
        {
			animator.SetBool("vacuming", false);
			addAmmo();
        }
	}

	public bool hasAmmo()
    {
		return ammo > 0;
    }

	public void addAmmo()
    {
		if (ammo < maxAmmo)
        {
			ammo += 1;
		}
    }

	public void removeAmmo()
    {
		if (ammo >= 0)
        {
			ammo -= 1;
		}
    }

	void Jump()
	{
		if (controller.isGrounded)
		{
			float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
			velocityY = jumpVelocity;

			int jumpHash = Animator.StringToHash("jump");
			animator.SetTrigger(jumpHash);
		}
	}

	float GetModifiedSmoothTime(float smoothTime)
	{
		if (controller.isGrounded)
		{
			return smoothTime;
		}

		if (airControlPercent == 0)
		{
			return float.MaxValue;
		}

		return smoothTime / airControlPercent;
	}
}