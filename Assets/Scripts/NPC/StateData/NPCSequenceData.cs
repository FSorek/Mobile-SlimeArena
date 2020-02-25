using UnityEngine;

[CreateAssetMenu(fileName = "NPC Sequence Data")]
public class NPCSequenceData : ScriptableObject
{
    [SerializeField] private int repeatAmount;

    public int RepeatAmount => repeatAmount;
}