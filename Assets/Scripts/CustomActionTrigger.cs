using UnityEngine;
using UnityEngine.Events;

public class CustomActionTrigger : MonoBehaviour
{
	[Header("Custom Action Trigger")]
	[SerializeField] private UnityEvent TriggerEnter;
	[SerializeField] private UnityEvent TriggerExit;

	[SerializeField] private LayerMask triggerLayers;

	[SerializeField] private bool destroyAfterTrigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
		var otherObject = collision.gameObject;

		if (((1 << otherObject.layer) & triggerLayers) == 0)
			return;

		TriggerEnter?.Invoke();
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
		var otherObject = collision.gameObject;

		if (((1 << otherObject.layer) & triggerLayers) == 0)
			return;

		TriggerExit?.Invoke();

		if (destroyAfterTrigger)
			Destroy(gameObject);
	}
}
