using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SoundTrigger soundTrigger;
    [SerializeField] private SoundSwitch soundSwitch;
    [SerializeField] private float minRandom;
    [SerializeField] private float maxRandom;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField, Range(0f,1f)] private float minTalkProbability;
    [SerializeField, Range(0f,1f)] private float maxTalkProbability;
    float timer = 0;
    float talkProbability;

    void Start()
    {
        timer = Random.Range(minRandom, maxRandom);
        talkProbability = minTalkProbability;
    }

    void Update()
    {
        float distanceToPlayer = distanceToTarget(transform.position, playerTransform.position);

        float t = Mathf.InverseLerp(maxDistance, minDistance, distanceToPlayer);
        talkProbability = Mathf.Lerp(minTalkProbability, maxTalkProbability, t);

        if (distanceToPlayer > 20)
            soundSwitch.Switch("Far");
        else if (distanceToPlayer <= 20 && distanceToPlayer > 10)
            soundSwitch.Switch("Mid");
        if (distanceToPlayer <= 10)
            soundSwitch.Switch("Near");

        if (timer <= 0)
        {
            playSound();
        }
        timer -= Time.deltaTime;
    }

    private float distanceToTarget(Vector3 from, Vector3 to)
    {
        Vector3 horizontalFrom = from - from.y * Vector3.up;
        Vector3 horizontalTo = to - to.y * Vector3.up;

        return Vector3.Distance(horizontalFrom, horizontalTo);
    }

    private void playSound()
    {
        bool talk = Random.value < talkProbability;
        if (talk)
            soundTrigger.PlaySound("Talk");
        else
            soundTrigger.PlaySound("Breath");
        timer = Random.Range(minRandom, maxRandom);
    }
}
