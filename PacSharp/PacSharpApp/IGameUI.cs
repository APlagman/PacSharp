namespace PacSharpApp
{
    static class GameUIConstants
    {
        internal const int GridScale = 2;
        internal const int WindowBorderWidth = 16;
        internal const int WindowBorderHeight = 39;
    }

    internal interface IGameUI
    {
        void OnNewGame();
        void Close();
    }
}