using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackVisualHelper : MonoBehaviour
{
    [SerializeField] private Sprite attackVisual;

    private ICanAttack attacker;
    private GameObject indicator;

    private void Start()
    {
        indicator = new GameObject("Visual Indicator");
        indicator.AddComponent<SpriteRenderer>().sprite = attackVisual;
        indicator.transform.SetParent(transform);

        attacker = GetComponent<ICanAttack>();
    }

    private void Update()
    {
        if(attacker.AttackDirection.magnitude <= 0)
            return;

        indicator.transform.position = (Vector2)attacker.AttackOrigin.position + attacker.AttackDirection;
    }
}