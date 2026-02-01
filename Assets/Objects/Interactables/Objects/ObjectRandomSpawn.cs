using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectRandomSpawn : MonoBehaviour
{
    [SerializeField] private Transform positionsTransform;
    [SerializeField] private GameObject[] objectsToSpawn;
    private List<Transform> positions;

    void Start()
    {
        positions = positionsTransform.GetComponentsInChildren<Transform>().ToList();
        positions.Remove(positionsTransform);

        foreach (GameObject g in objectsToSpawn)
        {
            int i = Random.Range(0, positions.Count);

            GameObject.Instantiate(g, positions[i].position, Quaternion.identity, this.transform);
            positions.RemoveAt(i);
        }
    }
}
