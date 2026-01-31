using UnityEngine;
using TEXADev.SerializedTypes;
using System;

public class SoundBooleanState : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, DictionaryBooleanState> states;

    void Start()
    {
        foreach (string s in states.Keys)
        {
            states[s].Set(states[s].Get());
        }
    }

    public void ToggleState(string name)
    {
        DictionaryBooleanState boolState = states.Get(name);
        if (boolState != default(DictionaryBooleanState))
            boolState.Toggle();
    }

    public void SetState(string name, bool value)
    {
        DictionaryBooleanState boolState = states.Get(name);
        if (boolState != default(DictionaryBooleanState))
            boolState.Set(value);
    }
}
