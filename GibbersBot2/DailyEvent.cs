namespace GibbersBot2;

public class DailyEvent
{
    private readonly Timer _timer;
    private readonly Func<Task> _task;

    public DailyEvent(int hour, int min, Func<Task> task)
    {
        _task = task;
        var timeTrigger = new TimeSpan(0, hour, min, 0);

        // Calculate initial delay
        var now = DateTime.Now;
        var triggerTime = now.Date.Add(timeTrigger); // Today at the timeTrigger
        if (triggerTime < now)
        {
            // If the time has already passed today, schedule for tomorrow
            triggerTime = triggerTime.AddDays(1);
        }

        var initialDelay = triggerTime - now;

        // Set the timer to trigger the action at the calculated time
        _timer = new Timer(TimerCallback, null, initialDelay, TimeSpan.FromDays(1)); // Repeat every 24 hours
    }

    private void TimerCallback(object? state)
    {
        _task.Invoke(); // Execute the action
    }

    public void Stop()
    {
        _timer.Change(Timeout.Infinite, Timeout.Infinite); // Stop the timer
    }
}