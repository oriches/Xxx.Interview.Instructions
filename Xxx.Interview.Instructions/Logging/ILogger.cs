namespace Xxx.Interview.Instructions.Logging
{
    public interface ILogger
    {
        void Info();

        void Info(string message);

        void Flush();
    }
}