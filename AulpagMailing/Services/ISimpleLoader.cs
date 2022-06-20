namespace AulpagMailing.Services
{
    public interface ISimpleLoader
    {
        bool IsLoading { get; }
        string TextToDisplay { get; }

        void StartLoading();
        void UpdateText(string text);
        void StopLoading();
    }
}
