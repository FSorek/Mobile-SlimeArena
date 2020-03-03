public class EntitySequence : IState
{
    private readonly SequenceData sequenceData;
    private int sequenceCounter;
    
    public bool CanContinueSequence { get; private set; }

    public EntitySequence(SequenceData sequenceData)
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