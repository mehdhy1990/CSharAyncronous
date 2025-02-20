namespace CsharpAsyncronos1;

public class DummyTask
{
    private readonly Lock _lock =new();
    private bool _completed;
    private Exception? _exception;
    private Action? _action;
    private ExecutionContext? _context;

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
                throw new Exception();
            }
        });
        return task;
    }

    public DummyTask ConintiueWith(Action action)
    {
        DummyTask task = new();
        lock (_lock)
        {
            if (_completed)
            {
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
            }
            else
            {
                _action = action;
                _context = ExecutionContext.Capture();
            }
        }

        return task;
    }

    public void SetResult() => CompleteTask(null);
  

    public void SetException(Exception exception) => CompleteTask(exception);
    

    private void CompleteTask(Exception? exception)
    {
        lock (_lock)
        {
            if (_completed)
                throw new InvalidOperationException("The task has already been completed cannot set the result.");
            _completed = true;
            _exception = exception;
            if (_action is not null)
            {
                if (_context is not null)
                {
                    _action.Invoke();
                }
                else
                {
                    ExecutionContext.Run(_context,state=>((Action?)state)?.Invoke() ,_action);
                }
            }
        }
    }
}