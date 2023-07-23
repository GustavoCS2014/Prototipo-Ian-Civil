namespace Management
{
    public static class SceneManager
    {
        public static void LoadScene(GameScene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }
    }

    public enum GameScene
    {
        MainMenu,
        Level1,
        BossLevel
    }
}
