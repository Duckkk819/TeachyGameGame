using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedObject : MonoBehaviour
{
    public bool interactable = true;
    public List<SimulatedComponent> components;
    public List<SimulatedScript> scripts;
    public Collider2D clickTrigger;
    private LayerMask layer;
    private InspectorController controller;
    private GameManager gameManager;

    public Sprite defaultSprite;
    public Sprite sprite1;

    [System.Serializable]
    public class SimulatedComponent
    {
        public Component realComponent;
        public VisualComponent visualComponent;
    }

    public void Start()
    {
        controller = InspectorController.Instance;
        gameManager = GameManager.Instance;
        layer = gameObject.layer;
        foreach (SimulatedScript script in scripts)
        {
            SetScriptEnabledStatus(script, script.isActiveAndEnabled);
        }

        InputManager.Instance.OnClick.AddListener(OnClick);

        // Its a 2D game, we dont want any depth. Plus it messes with click detection
        AlignZAxis();
    }

    private void AlignZAxis()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public bool IsComponentToggleable(SimulatedComponent component)
    {
        switch (component.realComponent)
        {
            case Collider2D:
                return true;
            case SpriteRenderer:
                return true;
            case Animator:
                return true;
            default:
                return false;
        }
    }

    public bool GetComponentEnabledStatus (SimulatedComponent component)
    {
        switch (component.realComponent)
        {
            case Collider2D collider:
                return collider.enabled;
            case SpriteRenderer renderer:
                return renderer.enabled;
            case Animator animator:
                return animator.enabled;
            default:
                return true;
        }
    }

    public void SetComponentEnabledStatus(SimulatedComponent component, bool enabled)
    {
        switch (component.realComponent)
        {
            case Collider2D collider:
                collider.enabled = enabled;
                gameObject.layer = enabled ? layer : 0;
                break;
            case SpriteRenderer renderer:
                renderer.enabled = enabled;
                break;
            case Animator animator:
                animator.enabled = enabled;
                break;
            default:
                Debug.Log("hello");
                break;
        }     
    }

    public void ToggleComponent(SimulatedComponent component)
    {
        SetComponentEnabledStatus(component, !GetComponentEnabledStatus(component));
    }


    public void SetScriptEnabledStatus(SimulatedScript script, bool enabled)
    {
        script.enabled = enabled;
        script.doCoroutines = enabled;
        script.doCollisionEvents = enabled;
    }

    public void ToggleScript(SimulatedScript script)
    {
        SetScriptEnabledStatus(script, !script.enabled);
    }


    public void OnClick()
    {
        Vector2 worldSpaceMousePos = controller.followCamera.controlledCamera.ScreenToWorldPoint(InputManager.Instance.mousePos);
        if (interactable && clickTrigger.bounds.Contains(worldSpaceMousePos))
        {
            controller.DisplayObject(this, defaultSprite, sprite1);
        }    
    }
}
