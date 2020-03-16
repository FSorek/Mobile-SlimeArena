using UnityEngine;

public class Playing : IState
{
    private ITakeDamage playerHealth;
    public bool IsGameFinished { get; private set; }
    public void StateEnter()
    {
        playerHealth = Object.FindObjectOfType<Player>().GetComponent<ITakeDamage>();
        playerHealth.OnTakeDamage += PlayerHealthOnTakeDamage;
        IsGameFinished = false;
    }

    private void PlayerHealthOnTakeDamage(int amount)
    {
        if (playerHealth.CurrentHealth <= 0)
            IsGameFinished = true;
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        playerHealth.OnTakeDamage -= PlayerHealthOnTakeDamage;
    }
}