using Game_logic;
using Helpers;
using Infrastructure;
using Player.Inputs;
using Player.Player_settings;
using Player.Player_stance;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;
using Weapons;

namespace Player
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(PlayerInput))]
	public class FirstPersonController : MonoBehaviour, IProgressReader, IProgressWriter
	{
		public Stances stances;
		public WeaponHolder weaponHolder;
		[Header("Player")] 
		public SpeedSettings speedSettings;

		[Space(10)]
		public float jumpHeight = 1.2f;
		public float gravity = -15.0f;

		[Space(10)]
		public float jumpTimeout = 0.1f;
		public float fallTimeout = 0.15f;

		public bool IsMoving => _input.move != Vector2.zero;
		public bool IsLooking => _input.look.magnitude > 1;
		public bool IsGrounded { get; set; } = true;
		public float groundedOffset = -0.14f;
		public float groundedRadius = 0.5f;
		public LayerMask groundLayers;

		[Header("Camera")]
		public Transform cameraHolder;
		public float recoilReduceSpeed = 10f;

		public float topClamp = 90.0f;
		public float bottomClamp = -90.0f;
		
		private float _speed;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		private Vector2 _recoil;

		private CharacterController _controller;
		private PlayerInputs _input;
		private WeaponBase _weapon;

		private const float Threshold = 0.01f;

		private void OnEnable()
		{
			weaponHolder.SwitchCurrentWeapon += SetCurrentWeapon;
		}

		private void SetCurrentWeapon(WeaponBase weapon)
		{
			if(_weapon)
				_weapon.OnShot -= SetRecoil;
			_weapon = weapon;
			if(_weapon)
				weapon.OnShot += SetRecoil;
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<PlayerInputs>();

			_jumpTimeoutDelta = jumpTimeout;
			_fallTimeoutDelta = fallTimeout;
		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			Move();
		}

		private void LateUpdate()
		{
			CameraRotation();
			UpdateRecoil();
		}

		private void UpdateRecoil()
		{
			cameraHolder.Rotate(Vector3.right * -_recoil.y);
			transform.Rotate(Vector3.up * _recoil.x);
			ReduceRecoil();
		}

		private void GroundedCheck()
		{
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
			IsGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			if (_input.look.sqrMagnitude >= Threshold)
			{
				float rotationX = _input.look.y * speedSettings.rotationSpeed;
				float rotationY = _input.look.x * speedSettings.rotationSpeed;

				rotationX = ClampAngle(rotationX, bottomClamp, topClamp);

				cameraHolder.Rotate(Vector3.right * rotationX);
				transform.Rotate(Vector3.up * rotationY);
			}
		}

		private void ReduceRecoil()
		{
			_recoil = Vector2.Lerp(_recoil, Vector2.zero, Time.deltaTime * recoilReduceSpeed);
		}

		private void Move()
		{
			float targetSpeed = GetTargetSpeed();
			
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedSettings.speedChangeRate);

				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			if (_input.move != Vector2.zero)
			{
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}

			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}

		private float GetTargetSpeed()
		{
			float targetSpeed = _input.sprint ? speedSettings.sprintSpeed : speedSettings.moveSpeed;

			targetSpeed *= SpeedModifier();
			
			return targetSpeed;
		}

		private float SpeedModifier()
		{
			return stances.currentStance switch
			{
				Stance.Crouch => speedSettings.crouchSpeedModifier,
				Stance.Prone => speedSettings.proneSpeedModifier,
				_ => 1
			};
		}

		private void JumpAndGravity()
		{
			if (IsGrounded)
			{
				_fallTimeoutDelta = fallTimeout;

				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
				}

				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				_jumpTimeoutDelta = jumpTimeout;

				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}

				_input.jump = false;
			}

			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += gravity * Time.deltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (IsGrounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
		}

		private void SetRecoil(Vector2 recoil) => 
			_recoil = recoil;

		public void Load(Progress progress)
		{
			transform.position = progress.PlayerState.position.ToVector3();
			transform.eulerAngles = progress.PlayerState.rotation.ToVector3();
			cameraHolder.eulerAngles = progress.PlayerState.cameraRotation.ToVector3();
		}
		
		public void Save(Progress progress)
		{
			progress.PlayerState.position = transform.position.ToVec3();
			progress.PlayerState.rotation = transform.eulerAngles.ToVec3();
			progress.PlayerState.cameraRotation = cameraHolder.eulerAngles.ToVec3();
		}
	}
}