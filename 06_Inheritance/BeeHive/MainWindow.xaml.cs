using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BeeHive;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Queen queen = new();
    DispatcherTimer timer = new();
    public MainWindow()
    {
        InitializeComponent();
        UpdateStatusReport();

        timer.Tick += Timer_Tick;
        timer.Interval = TimeSpan.FromSeconds(1.5f);
        timer.Start();
    }

    void Timer_Tick(object sender, EventArgs e)
    {
        WorkShift_Click(this, new RoutedEventArgs());
    }
    
    private void AssignJob_Click(object sender, RoutedEventArgs e)
    {
        queen.AssignBee(JobSelector.Text);
        UpdateStatusReport();
    }

    private void WorkShift_Click(object sender, RoutedEventArgs e)
    {
        queen.WorkTheNextShift();
        UpdateStatusReport();
    }

    void UpdateStatusReport()
    {
        StatusReport.Text = queen.StatusReport;
    }
}

static class HoneyVault
{
    const float NectarConversionRatio = .19f;
    const float LowLevelWarning = 10f;
    static float honey = 25f;
    static float nectar = 100f;

    public static void CollectNectar(float amount)
    {
        if (amount > 0) nectar += amount;
    }

    public static void ConvertNectarToHoney(float amount)
    {
        amount = Math.Min(amount, nectar);
        nectar -= amount;
        honey += amount * NectarConversionRatio;
    }

    public static bool ConsumeHoney(float amount)
    {
        if (honey < amount) return false;
        honey -= amount;
        return true;
    }

    public static string StatusReport
    {
        get
        {
            var status = $"{honey:0.0} units of honey\n{nectar:0.0} units of nectar";
            var warnings = "";
            if (honey < LowLevelWarning) warnings += "\nLOW HONEY - Add a honey manufacturer";
            if (nectar < LowLevelWarning) warnings += "\nLOW NECTAR - Add a nectar collector";
            return status + warnings;
        }
    }
}

abstract class Bee
{
    public string Job { get; private set; }
    protected abstract float CostPerShift { get; };

    protected Bee(string job)
    {
        Job = job;
    }

    public void WorkTheNextShift()
    {
        if (HoneyVault.ConsumeHoney(CostPerShift)) 
            DoJob();
    }

    protected abstract void DoJob();
}

class Queen : Bee
{
    const float EggsPerShift = 0.45f;
    const float HoneyPerUnassignedWorker = 0.5f;

    float eggs;
    float unassignedWorkers = 3;
    Bee[] workers = Array.Empty<Bee>();
    public string StatusReport { get; private set; }
    protected override float CostPerShift { get; } = 2.15f;

    public Queen() : base("Queen")
    {
        AssignBee("Nectar Collector");
        AssignBee("Honey Manufacturer");
        AssignBee("Egg Care");
    }

    protected override void DoJob()
    {
        eggs += EggsPerShift;
        foreach (var worker in workers)
            worker.WorkTheNextShift();
        HoneyVault.ConsumeHoney(HoneyPerUnassignedWorker * unassignedWorkers);
        UpdateStatusReport();
    }

    public void AddWorker(Bee worker)
    {
        if (unassignedWorkers < 1) return;
        unassignedWorkers--;
        Array.Resize(ref workers, workers.Length + 1);
        workers[^1] = worker;
    }

    public void AssignBee(string job)
    {
        switch (job)
        {
            case "Egg Care":
                AddWorker(new EggCare(this));
                break;
            case "Honey Manufacturer":
                AddWorker(new HoneyManufacturer());
                break;
            case "Nectar Collector":
                AddWorker(new NectarCollector());
                break;
            default:
                return;
        }
        UpdateStatusReport();
    }

    void UpdateStatusReport()
    {
        StatusReport = $"HoneyVault StatusReport:\n{HoneyVault.StatusReport}\n" +
                       $"\nEgg count: {eggs: 0.0}\nUnassigned Workers: {unassignedWorkers:0.0}\n" +
                       $"\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS: {workers.Length}";
    }

    string WorkerStatus(string job)
    {
        var count = workers.Count(worker => worker.Job == job);
        return $"{count} {job} bee{(count == 1 ? "" : "s")}";
    }

    public void CareForEggs(float eggsToConvert)
    {
        if (eggs < eggsToConvert) return;

        eggs -= eggsToConvert;
        unassignedWorkers += eggsToConvert;
    }

}

class HoneyManufacturer : Bee
{
    const float NectarProcessedPerShift = 33.25f;
    protected override float CostPerShift { get; } = 1.7f;

    public HoneyManufacturer() : base("Honey Manufacturer")
    {
    }

    protected override void DoJob()
    {
        HoneyVault.ConvertNectarToHoney(NectarProcessedPerShift);
    }
}

class EggCare : Bee
{
    const float CareProgressPerShift = 0.15f;
    Queen Queen { get; }
    protected override float CostPerShift { get; } = 1.35f;

    public EggCare(Queen queen) : base("Egg Care")
    {
        Queen = queen;
    }

    protected override void DoJob()
    {
        Queen.CareForEggs(CareProgressPerShift);
    }
}

class NectarCollector : Bee
{
    const float NectarCollectedPerShift = 33.25f;
    protected override float CostPerShift { get; } = 1.95f;

    public NectarCollector() : base("Nectar Collector")
    {
    }

    protected override void DoJob()
    {
        HoneyVault.CollectNectar(NectarCollectedPerShift);
    }
}