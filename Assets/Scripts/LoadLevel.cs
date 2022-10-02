using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void Load(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Load(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadNext()
    {
        var next = SceneManager.GetActiveScene().buildIndex + 1;
        if(SceneManager.sceneCount > next)
            SceneManager.LoadScene(next);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
