namespace Observable
{
    public struct EmptyParams : IObserverParams
    {
        public static EmptyParams Empty => new EmptyParams();
    }
}