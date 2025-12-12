using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    InputAction menuAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuAction = InputSystem.actions.FindAction("Menu");

        menuAction.performed += MenuAction_performed;
    }

    bool menuIsDisplayed = false;
    private async void MenuAction_performed(InputAction.CallbackContext obj)
    {
        if (menuIsDisplayed == false)
        {
            menuAction.performed -= MenuAction_performed;
            Time.timeScale = 0;
            await SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
            menuIsDisplayed = true;
            menuAction.performed += MenuAction_performed;
        }
        else
        {
            menuAction.performed -= MenuAction_performed;
            await SceneManager.UnloadSceneAsync("Menu");
            Time.timeScale = 1;
            menuIsDisplayed = false;
            menuAction.performed += MenuAction_performed;
        }
    }

    private void OnDestroy()
    {
        menuAction.performed -= MenuAction_performed;
    }
}
