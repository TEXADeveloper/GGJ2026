using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaskRandomSpawn : MonoBehaviour
{
    [SerializeField] private Transform positionsTransform;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int amount = 2;
    private List<Transform> positions;

    void Start()
    {
        positions = positionsTransform.GetComponentsInChildren<Transform>().ToList();
        positions.Remove(positionsTransform);

        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, positions.Count);

            GameObject.Instantiate(objectToSpawn, positions[index].position, Quaternion.identity, this.transform);
            positions.RemoveAt(i);
        }
    }
}
