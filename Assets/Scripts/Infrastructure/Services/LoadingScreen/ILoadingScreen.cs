namespace LoadingScreen
{
    public interface ILoadingScreen
    {
        void Show();
        void Hide();
        void UpdateLoadingProgress(float progress);
    }
}