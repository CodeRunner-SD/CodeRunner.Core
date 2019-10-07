namespace CodeRunner.Extensions
{
    public interface IHost
    {
        void Shutdown();

        void Restart();

        void SendMessage(string message);
    }
}
