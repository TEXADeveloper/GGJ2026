using TMPro;
using UnityEngine;

public class ObjectDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int amount = 0;
    private int max = 0;

    public void SetMax(int value)
    {
        max = value;

        text.text = amount + " / " + max;
    }

    public void PickObject()
    {
        amount++;

        text.text = amount + " / " + max;
    }
}
