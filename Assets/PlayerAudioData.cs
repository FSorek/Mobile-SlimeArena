using UnityEngine;

[CreateAssetMenu(fileName = "Player Audio Data")]
public class PlayerAudioData : ScriptableObject
{
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip hitSound;

    public AudioClip AttackSound => attackSound;
    public AudioClip HitSound => hitSound;
}