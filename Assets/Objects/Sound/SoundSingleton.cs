using System.Collections;
using UnityEngine;

public class SoundSingleton : MonoBehaviour
{
    private const float FADE_TIME = 2;
    private const float MAX_DISTANCE = 35;

    public static SoundSingleton instance;

    [SerializeField] private AK.Wwise.RTPC distanceParameter;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(instance.gameObject);
    }

    public void SetMaxDistance()
    {
        StartCoroutine(distanceFade());
    }

    private IEnumerator distanceFade()
    {
        float time = 0;
        while (time <= FADE_TIME)
        {
            distanceParameter.SetGlobalValue((time/FADE_TIME) * MAX_DISTANCE);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
