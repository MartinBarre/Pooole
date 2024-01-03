using UnityEngine;
using Utils;

public class TriggerDisableRenderer : MonoBehaviour
{
    public Renderer rendererToDisabled;
    public bool activateOnEnter;

    private void Start()
    {
        rendererToDisabled.enabled = !activateOnEnter;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag(Tag.CHICKEN))
        {
            rendererToDisabled.enabled = activateOnEnter;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag(Tag.CHICKEN))
        {
            rendererToDisabled.enabled = !activateOnEnter;
        }
    }
}