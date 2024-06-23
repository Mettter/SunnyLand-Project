using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }
    
    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadBossScene()
    {
        SceneManager.LoadScene(3);
    }

}
