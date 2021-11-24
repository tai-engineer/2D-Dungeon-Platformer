using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Input/InputReader",fileName ="New Input Reader")]
public class InputReaderSO : ScriptableObject, GameInput.IPlayerInputActions
{
    GameInput _gameInput;

    public event UnityAction crouchEvent = delegate { };
    public event UnityAction<bool> jumpEvent = delegate { };
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction<bool> shootEvent = delegate { };
    public event UnityAction<bool> sprintEvent = delegate { };
    public event UnityAction<bool> throwEvent = delegate { };
    void OnEnable()
    {
        if(_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.PlayerInput.SetCallbacks(this);
        }

        _gameInput.PlayerInput.Enable();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            crouchEvent.Invoke(); 
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            jumpEvent.Invoke(true);
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            jumpEvent.Invoke(false);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            jumpEvent.Invoke(false);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            shootEvent.Invoke(true); 
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            shootEvent.Invoke(false);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            sprintEvent.Invoke(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            sprintEvent.Invoke(false);
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            throwEvent.Invoke(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            throwEvent.Invoke(false);
        }
    }
}
