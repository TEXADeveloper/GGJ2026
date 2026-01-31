using UnityEngine;
using TEXADev.SerializedTypes;

public class SoundSwitch : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, AK.Wwise.Switch> switches;

    public void Switch(string name)
    {
        AK.Wwise.Switch _akSwitch = switches[name];
        if (_akSwitch != default(AK.Wwise.Switch))
            _akSwitch.SetValue(gameObject);
    }
}
