using UnityEngine;

public class Playing : IState
{
    private ITakeDamage playerHealth;
    public bool IsGameOver { get; private set; }
    public void StateEnter()
    {
        playerHealth = Object.FindObjectOfType<Player>().GetComponent<ITakeDamage>();
        playerHealth.OnTakeDamage += PlayerHealthOnTakeDamage;
        IsGameOver = false;
    }

    private void PlayerHealthOnTakeDamage(int amount)
    {
        if (playerHealth.CurrentHealth <= 0)
            IsGameOver = true;
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        playerHealth.OnTakeDamage -= PlayerHealthOnTakeDamage;
    }
}