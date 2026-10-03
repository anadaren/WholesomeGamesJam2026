using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void GoToScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}