namespace CsharpAsyncronos1;

public class DummyTask
{
    private readonly Lock _lock;
    private bool _completed;
    private Exception? _exception;

    public bool IsCompleted
    {
        get
        {
            lock (_lock)
            {
                return _completed;
            }
        }
    }

    public static DummyTask Run(Action action)
    {
        DummyTask task = new();
        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                action();
                task.SetResult();
            }
            catch (Exception e)
            {
                task.SetException(e);
            }
        });
        return task;
    }

    public void SetResult()
    {
        lock (_lock)
        {
            if (_completed)
            {
                throw new InvalidOperationException("The task has already been completed.");
                _completed = true;
            }
        }
    }

    public void SetException(Exception exception)
    {
        lock (_lock)
        {
            if (_completed)
            {
                throw new InvalidOperationException("The task has already been completed cannot set the result.");
            }

            _exception = exception;
        }
    }
}