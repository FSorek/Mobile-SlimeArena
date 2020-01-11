using UnityEngine;

[CreateAssetMenu(fileName = "NPC Move Data")]
public class NPCMoveData : ScriptableObject
{
    public float MoveSpeed = 5f;
    public float RepositionDistance = 2f;
}