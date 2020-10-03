namespace Observable
{
    public interface IObserverNotify<TObserver, TParams>
          where TParams : IObserverParams
          where TObserver : IObserver<TObserver, TParams>
    {
    }

    public interface IObserverNotifyEmpty<TObserver> : IObserverNotify<TObserver, EmptyParams>
        where TObserver : IObserver<TObserver, EmptyParams>
    {
    }
}