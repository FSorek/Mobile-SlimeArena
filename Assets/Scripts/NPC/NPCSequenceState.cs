public class NPCSequenceState : IState
{
    private readonly int sequenceIterations;
    private int sequenceCounter;
    
    public bool CanContinueSequence { get; private set; }

    public NPCSequenceState(int sequenceIterations)
    {
        this.sequenceIterations = sequenceIterations;
        CanContinueSequence = true;
    }
    public void StateEnter()
    {
        sequenceCounter++;
        CanContinueSequence = sequenceCounter < sequenceIterations;
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        if (!CanContinueSequence)
        {
            sequenceCounter = 0;
            CanContinueSequence = true;
        }
    }
}