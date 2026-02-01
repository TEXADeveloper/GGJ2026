using UnityEngine;

public class Carpet : MonoBehaviour
{
    [SerializeField] private SoundSwitch soundSwitch;

    void OnTriggerEnter(Collider other)
    {
        soundSwitch.Switch("Carpet");
    }

    void OnTriggerExit(Collider other)
    {
        soundSwitch.Switch("Floor");
    }
}
