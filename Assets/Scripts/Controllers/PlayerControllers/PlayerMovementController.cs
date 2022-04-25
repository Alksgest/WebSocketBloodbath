using System.Collections;
using Controllers.AnimationStates;
using Controllers.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace Controllers.PlayerControllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 200f;

        [SerializeField] private Animator animator;
        [SerializeField] private PlayerStatsController playerStatsController;

        private PlayerControlInputActions _control;

        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Attack = Animator.StringToHash("Attack");

        private void Awake()
        {
            _control = new PlayerControlInputActions();
            _control.Player.Mouse.started += HandleFightActionStarted;
            _control.Player.Mouse.canceled += HandleFightActionCanceled;
        }

        private void HandleFightActionStarted(InputAction.CallbackContext context)
        {
            if (MainUIController.Instance.IsUIBlocking)
            {
                return;
            }
            
            SetAttackAnimation(AttackAnimationState.Attack);
            // StartCoroutine(FinishAttackAnimation());
        }

        private IEnumerator FinishAttackAnimation()
        {
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }

            SetAttackAnimation(AttackAnimationState.NoAction);
        }

        private void HandleFightActionCanceled(InputAction.CallbackContext context)
        {
            SetAttackAnimation(AttackAnimationState.NoAction);
        }

        private void OnEnable()
        {
            _control.Enable();
        }

        private void OnDisable()
        {
            _control.Disable();
        }

        private void Update()
        {
            HandleStaminaChanges();
            
            if (MainUIController.Instance.IsUIBlocking)
            {
                return;
            }

            HandleMovement();
            HandleRotation();
        }

        private void HandleStaminaChanges()
        {
            var moveState = (MoveAnimationState)animator.GetInteger(Move);
            switch (moveState)
            {
                case MoveAnimationState.Run: 
                case MoveAnimationState.RunForwardLeft: 
                case MoveAnimationState.RunForwardRight:
                    ReduceStamina();
                    break;
                case MoveAnimationState.NoAction:
                    IncreaseStamina();
                    break;
            }
        }
        
        private void HandleRotation()
        {
            var rotationDirection = _control.Player.Rotate.ReadValue<float>();

            switch (rotationDirection)
            {
                case -1:
                    transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                    break;
                case 1:
                    transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
                    break;
            }
        }

        private void HandleMovement()
        {
            var direction = _control.Player.Move.ReadValue<Vector2>();
            var shiftW = (int) Mathf.Round(_control.Player.Run.ReadValue<float>());
            var wA = _control.Player.MoveForwardLeft.ReadValue<float>();
            var wD = _control.Player.MoveForwardRight.ReadValue<float>();
            var sA = _control.Player.MoveBackwardLeft.ReadValue<float>();
            var sD = _control.Player.MoveBackwardRight.ReadValue<float>();

            var stamina = playerStatsController.Player.PlayerStats.Stamina.Value;

            _ = direction switch
            {
                {x: < 0, y: 0} => SetMoveAnimation(MoveAnimationState.MoveLeft),
                {x: > 0, y: 0} => SetMoveAnimation(MoveAnimationState.MoveRight),
                {y: > 0} when wA > 0 && shiftW > 0 && stamina > 0 =>
                    SetMoveAnimation(MoveAnimationState.RunForwardLeft),
                {y: > 0} when wD > 0 && shiftW > 0 && stamina > 0 =>
                    SetMoveAnimation(MoveAnimationState.RunForwardRight),
                {y: > 0} when wA > 0 => SetMoveAnimation(MoveAnimationState.MoveForwardLeft),
                {y: > 0} when wD > 0 => SetMoveAnimation(MoveAnimationState.MoveForwardRight),
                {y: > 0} when shiftW > 0 && stamina > 0 => SetMoveAnimation(MoveAnimationState.Run),
                {y: > 0} => SetMoveAnimation(MoveAnimationState.Move),
                {y: < 0} when sA > 0 => SetMoveAnimation(MoveAnimationState.MoveBackwardLeft),
                {y: < 0} when sD > 0 => SetMoveAnimation(MoveAnimationState.MoveBackwardRight),
                {y: < 0} => SetMoveAnimation(MoveAnimationState.MoveBackward),
                _ => SetMoveAnimation(MoveAnimationState.NoAction)
            };
        }

        private void IncreaseStamina()
        {
            playerStatsController.IncreaseStamina(Time.deltaTime *
                                                playerStatsController.Player.PlayerStats.Stamina.RecoveryRate);
        }

        private void ReduceStamina()
        {
            playerStatsController.ReduceStamina(Time.deltaTime *
                                                playerStatsController.Player.PlayerStats.Stamina.ConsumptionRate);
        }

        private bool SetJumpAnimation(bool state)
        {
            animator.SetBool(Jump, state);

            return true;
        }

        private bool SetAttackAnimation(AttackAnimationState state)
        {
            animator.SetInteger(Attack, (int) state);

            return true;
        }

        private bool SetMoveAnimation(MoveAnimationState state)
        {
            animator.SetInteger(Move, (int) state);

            return true;
        }
    }
}