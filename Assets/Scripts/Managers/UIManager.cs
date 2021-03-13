using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingUI;

    private void OnEnable()
    {
        EventManager.onRequestSent += EnableLoadingState;
        EventManager.onRequestCompleted += DisableLoadingState;
    }

    private void OnDisable()
    {
        EventManager.onRequestSent -= EnableLoadingState;
        EventManager.onRequestCompleted -= DisableLoadingState;
    }

    //Manipulation of all necesarry UI for the loading state
    private void EnableLoadingState()
    {
        loadingUI.SetActive(true);
    }

    private void DisableLoadingState()
    {
        loadingUI.SetActive(false);
    }
}
