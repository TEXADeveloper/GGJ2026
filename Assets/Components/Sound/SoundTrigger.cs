using UnityEngine;
using TEXADev.SerializedTypes;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, AK.Wwise.Event> sounds;

    public void PlaySound(string name)
    {
        AK.Wwise.Event _akEvent = sounds[name];
        if (_akEvent != default(AK.Wwise.Event))
            _akEvent.Post(this.gameObject);
    }
}
