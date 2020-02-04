using UnityEngine;

[CreateAssetMenu(fileName = "NPC Move Data")]
public class NPCMoveData : ScriptableObject
{
    public float MoveSpeed = 5f;
    public float RepositionSpeed = 2f;
    public float RepositionDistance = 2f;
}