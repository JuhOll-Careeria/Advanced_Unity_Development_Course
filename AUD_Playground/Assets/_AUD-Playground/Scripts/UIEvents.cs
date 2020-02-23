using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private CustomEvent[] Events;

    private void Start()
    {
        foreach (CustomEvent evt in Events)
        {
            evt._InputAction.action.performed += ctx => evt._Event.Invoke();
        }
    }

    public void ToggleInGameOptions(GameObject Target)
    {
        Target.transform.rotation = Quaternion.Euler(0, 0, 0);
        GameManager.Instance.TogglePlayerInput(Target.activeInHierarchy);
        Target.SetActive(!Target.activeInHierarchy);
        Cursor.visible = Target.activeInHierarchy;

        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Confined;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnEnable()
    {
        foreach (CustomEvent evt in Events)
        {
            evt._InputAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (CustomEvent evt in Events)
        {
            evt._InputAction.action.Disable();
        }
    }
}
