using UnityEngine;

[CreateAssetMenu(fileName = "Sequence Data")]
public class SequenceData : ScriptableObject
{
    [SerializeField] private int repeatAmount;

    public int RepeatAmount => repeatAmount;
}