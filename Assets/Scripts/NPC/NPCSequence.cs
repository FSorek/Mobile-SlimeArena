public class NPCSequence : IState
{
    private readonly NPCSequenceData sequenceData;
    private int sequenceCounter;
    
    public bool CanContinueSequence { get; private set; }

    public NPCSequence(NPCSequenceData sequenceData)
    {
        this.sequenceData = sequenceData;
        CanContinueSequence = true;
    }
    public void StateEnter()
    {
        sequenceCounter++;
        CanContinueSequence = sequenceCounter < sequenceData.RepeatAmount;
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