public class ReadTask : StopTimeTask
{
    protected override void BeginTask()
    {
        TrainingPanel.SetContinueButtonActive(true);
        TrainingPanel.SetCancelButtonActive(true);
        TrainingPanel.SetGameInteractable(false);
        TrainingPanel.ContinueButtonClicked += Complete;
        InputHandler.SetUIMode();
        base.BeginTask();
    }

    protected override void OnComplete()
    {
        TrainingPanel.ContinueButtonClicked -= Complete;
        base.OnComplete();
    }
}
