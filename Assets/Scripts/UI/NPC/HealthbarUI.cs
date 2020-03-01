using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour, IGameObjectPooled
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float offsetPositionY;
    [SerializeField] private float updateSpeed = 0.5f;
    private Camera mainCamera;
    private Transform attachedTransform;
    private Health attachedHealth;

    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    internal void SetHealth(EnemyNPC npc)
    {
        if (attachedTransform != null)
            npc.Health.OnTakeDamage -= HandleHealthChanged;

        attachedTransform = npc.transform;
        attachedHealth = npc.Health;
        healthSlider.value = 1f;
        attachedHealth.OnTakeDamage += HandleHealthChanged;
    }

    private void HandleHealthChanged(int damage)
    {
        if(!gameObject.activeSelf) return;
        float pct = attachedHealth.CurrentHealth / (float) attachedHealth.MaxHealth;
        StopAllCoroutines();
        StartCoroutine(ResizeToPercentage(pct));
    }

    private IEnumerator ResizeToPercentage(float pct)
    {
        float preChangePercentage = healthSlider.value;
        float elapsed = 0f;

        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(preChangePercentage, pct, elapsed / updateSpeed);
            yield return null;
        }

        healthSlider.value = pct;
    }

    private void LateUpdate()
    {
        if (attachedTransform != null)
            transform.position = mainCamera.WorldToScreenPoint(attachedTransform.position + Vector3.up * offsetPositionY);
    }
}