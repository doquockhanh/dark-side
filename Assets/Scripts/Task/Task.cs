public abstract class Task
{
    public string TaskName { get; protected set; }
    public bool IsCompleted { get; protected set; }
    public event System.Action<Task> OnTaskCompleted;

    public abstract void CheckCompletion();

    protected void CompleteTask()
    {
        IsCompleted = true;
        OnTaskCompleted?.Invoke(this);
    }
}