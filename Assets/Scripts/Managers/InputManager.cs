using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Button refreshButton = null;

    private void OnEnable()
    {
        EventManager.onRequestSent += DisableInputs;
        EventManager.onRequestCompleted += EnableInputs;
    }

    private void OnDisable()
    {
        EventManager.onRequestSent -= DisableInputs;
        EventManager.onRequestCompleted -= EnableInputs;
    }

    //Manipulation of all necesarry inputs for the loading state
    private void EnableInputs()
    {
        refreshButton.interactable = true;
    }

    private void DisableInputs()
    {
        refreshButton.interactable = false;
    }
}
