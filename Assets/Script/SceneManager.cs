using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �л���ָ������
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // �˳���Ϸ����ѡ��
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �ڱ༭�����˳�����ģʽ
#endif
    }
}
