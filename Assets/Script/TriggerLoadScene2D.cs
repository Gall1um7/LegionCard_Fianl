using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneChange : MonoBehaviour
{
    [Header("指定能触发的物体（例如 Player）")]
    public GameObject targetObject;

    [Header("要切换到的场景名称（需添加到 Build Settings）")]
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是指定物体
        if (other.gameObject == targetObject)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}