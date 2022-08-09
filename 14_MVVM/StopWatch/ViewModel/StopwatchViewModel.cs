using StopWatch.Model;

namespace StopWatch.ViewModel;

class StopwatchViewModel
{
    readonly StopwatchModel _model;

    public StopwatchViewModel()
    {
        _model = new();
    }

    public void StartStop() => _model.Running = !_model.Running;
    public void Reset() => _model.Reset();

    public void LapTime()
    {
        _model.SetLapTime();
    }

    string Hours => _model.Elapsed.Hours.ToString("D2");
    string Minutes => _model.Elapsed.Minutes.ToString("D2");
    string Seconds => _model.Elapsed.Seconds.ToString("D2");
    string Tenths => ((int)(_model.Elapsed.Milliseconds / 100M)).ToString();
    string CurrentTime => $"{Hours}:{Minutes}:{Seconds}.{Tenths}";


    string LapHours => _model.LapTime.Hours.ToString("D2");
    string LapMinutes => _model.LapTime.Minutes.ToString("D2");
    string LapSeconds => _model.LapTime.Seconds.ToString("D2");
    string LapTenths => ((int)(_model.LapTime.Milliseconds / 100M)).ToString();
    string CurrentLapTime => $"{LapHours}:{LapMinutes}:{LapSeconds}.{LapTenths}";

    public string TimeAndLap => $"{CurrentTime} - lap time {CurrentLapTime}";
}
