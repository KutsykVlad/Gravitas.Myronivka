namespace Gravitas.Platform.Web.Manager.Test
{
    public interface ITestManager
    {
        bool WeighbridgeTestTask(long nodeId, int timeDelay, int executionTime);
        bool TerminateWeighbridgeTestTask();
    }
}