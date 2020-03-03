using UnityEngine;

[CreateAssetMenu(fileName = "State Audio Data")]
public class StateAudioData : ScriptableObject
{
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip deathSound;
    public AudioClip AttackSound => attackSound;
    public AudioClip DeathSound => deathSound;
}