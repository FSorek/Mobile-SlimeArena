public class NPCDodge : IState
{
    private readonly NPCDodger dodger;

    public NPCDodge(NPCDodger dodger)
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

