using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    void FadeIN()
    {
        SceneManager.LoadScene(sceneName);
    }
}
