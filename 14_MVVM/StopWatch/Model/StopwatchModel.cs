namespace StopWatch.Model;

class StopwatchModel
{
    DateTime _startTime;
    bool _isPaused = true;
    DateTime _pausedAt = DateTime.MinValue;
    TimeSpan _totalPausedTime = TimeSpan.Zero;

    public TimeSpan LapTime { get; private set; }

    public void SetLapTime() => LapTime = Elapsed;

    /// <summary>
    /// The constructor resets the Stopwatch
    /// </summary>
    public StopwatchModel() => Reset();

    /// <summary>
    /// Returns true if Stopwatch is running
    /// </summary>
    public bool Running
    {
        get => (_startTime != DateTime.MinValue) && !_isPaused;
        set
        {
            if (value)
            {
                _isPaused = false;

                if (_pausedAt != DateTime.MinValue)
                    _totalPausedTime += DateTime.Now - _pausedAt;
                if (_startTime == DateTime.MinValue)
                    _startTime = DateTime.Now;
            }
            else
            {
                _isPaused = true;
                _pausedAt = DateTime.Now;
            }
        }
    }

    /// <summary>
    /// Returns elapsed time, or zero if it is not running
    /// </summary>
    public TimeSpan Elapsed => Running ? DateTime.Now - _startTime - _totalPausedTime : _pausedAt - _startTime - _totalPausedTime;

    /// <summary>
    /// Resets Stopwatch startTime to DateTime.MinValue
    /// </summary>
    public void Reset()
    {
        _startTime = DateTime.MinValue;
        _pausedAt = DateTime.MinValue;
        _totalPausedTime = TimeSpan.Zero;
        _isPaused = true;
        LapTime = TimeSpan.Zero;
    }
}
