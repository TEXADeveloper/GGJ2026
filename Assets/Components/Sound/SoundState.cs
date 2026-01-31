using UnityEngine;
using TEXADev.SerializedTypes;

public class SoundState : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, AK.Wwise.State> states;

    public void Switch(string name)
    {
        AK.Wwise.State _akState = states.Get(name);
        if (_akState != default(AK.Wwise.State))
            _akState.SetValue();
    }
}
