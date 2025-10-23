using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneChange : MonoBehaviour
{
    [Header("ָ���ܴ��������壨���� Player��")]
    public GameObject targetObject;

    [Header("Ҫ�л����ĳ������ƣ�����ӵ� Build Settings��")]
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ����Ƿ���ָ������
        if (other.gameObject == targetObject)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}