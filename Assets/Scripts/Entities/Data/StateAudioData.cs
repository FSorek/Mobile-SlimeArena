using UnityEngine;

[CreateAssetMenu(fileName = "State Audio Data")]
public class StateAudioData : ScriptableObject
{
    [SerializeField] private AudioClip attackSound;
    public AudioClip AttackSound => attackSound;
}