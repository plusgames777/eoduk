using UnityEngine.SceneManagement;

namespace Eoduk.Core
{
    public static class SceneLoader
    {
        public static void Load(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}
