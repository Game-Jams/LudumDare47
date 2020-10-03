namespace Observable
{
    public interface IObserver<TSelf, in TParams>
         where TSelf : IObserver<TSelf, TParams>
         where TParams : IObserverParams
    {
        void Completed(TParams parameters);
    }
}