using Observable;
using UnityEngine;

public class SessionInitializer : MonoBehaviour, 
    IObserverNotifyEmpty<ISessionStartedListener>
{
    public void OnIntroEnded()
    {
        this.NotifyListeners<ISessionStartedListener>();
    }
}
