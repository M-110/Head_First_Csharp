using StopWatch.ViewModel;

namespace StopWatch.View;

public class StopwatchView
{
    StopwatchViewModel _viewModel = new();
    bool _quit = false;

    /// <summary>
    /// Clear console and display timer.
    /// </summary>
    public StopwatchView()
    {
        ClearScreenAndAddHelpMessage();
        TimerCallback timerCallBack = UpdateTimerCallback;
        var timer = new Timer(timerCallBack, null, 0, 10);
        while (!_quit)
            Thread.Sleep(100);
        Console.CursorVisible = true;
    }

    /// <summary>
    /// Clear screen, add help message and hide cursor
    /// </summary>
    static void ClearScreenAndAddHelpMessage()
    {
        Console.Clear();
        Console.CursorTop = 3;
        Console.WriteLine("Space to start, R to reset, L for lap time, any other key to quit");
        Console.CursorVisible = false;
    }

    private void UpdateTimerCallback(object? state)
    {
        if (Console.KeyAvailable)
        {
            switch(Console.ReadKey(true).Key)
            {
                case ConsoleKey.Spacebar:
                    _viewModel.StartStop();
                    break;
                case ConsoleKey.R:
                    _viewModel.Reset();
                    break;
                case ConsoleKey.L:
                    _viewModel.LapTime();
                    break;
                default:
                    Console.CursorVisible = true;
                    Console.CursorLeft = 0;
                    Console.CursorTop = 5;
                    _quit = true;
                    break;
            }
        }
        WriteCurrentTime();
    }

    void WriteCurrentTime()
    {
        Console.CursorTop = 1;
        Console.CursorLeft = 23;
        Console.Write(_viewModel.TimeAndLap);
    }
}
