using UnityEngine;

[System.Serializable]
public class DictionaryBooleanState
{
    [SerializeField] bool isOn = false;
    [SerializeField] AK.Wwise.State onState;
    [SerializeField] AK.Wwise.State offState;

    public void Toggle()
    {
        Set(!isOn);
    }

    public void Set(bool value)
    {
        isOn = value;
        if (isOn)
            onState.SetValue();
        else
            offState.SetValue();
    }

    public bool Get()
    {
        return isOn;
    }
}
