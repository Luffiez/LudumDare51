using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    private void Start()
    {
        var scene = SceneManager.GetActiveScene();
        GetComponent<TMP_Text>().text = $"Level {scene.buildIndex}";
    }
}
