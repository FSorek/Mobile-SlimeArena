using UnityEngine;

public class CastingAbility : IState
{
    private readonly IAbility ability;
    private readonly IAbilityPool pool;
    private float lastTickTime;

    public bool CanCast => pool.CurrentPoolAmount - ability.Cost >= 0;

    //public double CurrentPoolPercentage => currentPool / abilityData.MaxPoolAmount;
    public CastingAbility(IAbility ability, IAbilityPool pool)
    {
        this.ability = ability;
        this.pool = pool;
    }
    public void StateEnter()
    {
        ability.StartedCasting();
    }

    public void ListenToState()
    {
        if (Time.time - lastTickTime >= ability.TickTime)
        {
            ability.Tick();
            pool.Reduce(ability.Cost);
            lastTickTime = Time.time;
        }
    }

    public void StateExit()
    {
        ability.FinishedCasting();
    }
}