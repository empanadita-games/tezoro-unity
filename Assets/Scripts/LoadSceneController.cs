using System.Collections;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    [SerializeField] private float sceneTransitionDuration = 2f;

    [Button]
    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCount;
        int nextScene = 0;

        if (currentScene <= sceneCount)
            nextScene = currentScene + 1;
        else
        {
            print("Its last scene");
            return;
        }

        StartCoroutine(LoadSceneCoroutine(nextScene));
    }

    private IEnumerator LoadSceneCoroutine(int scene)
    {
        UIController.Instance.Fade.PlayFadeOut(sceneTransitionDuration);
        yield return new WaitForSeconds(sceneTransitionDuration);
        SceneManager.LoadScene(scene);
    }

}
