namespace Management
{
    public static class SceneManager
    {
        public static void LoadScene(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }
    }

    public enum Scene
    {
        MainMenu,
        Level1
    }
}
