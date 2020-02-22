public class NPCRepositionState : IState
{
    private readonly NPCDodger dodger;

    public NPCRepositionState(NPCDodger dodger)
    {
        this.dodger = dodger;
    }
    public void StateEnter()
    {
        dodger.Dodge();
    }

    public void ListenToState()
    {

    }

    public void StateExit()
    {

    }
}

