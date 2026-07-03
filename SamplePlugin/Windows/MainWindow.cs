using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;

namespace PvPTimer.Windows;

public class MainWindow : Window, IDisposable
{

    public MainWindow()
        : base("PvP Timer", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.Separator();
        ImGui.Text("Crystalline Conflict");
        ImGui.Text(RotationCalculator.GetCrystallineConflictTimeUntilNext().ToString(@"hh\:mm\:ss"));
        ImGui.Text(RotationCalculator.GetCurrentCrystallineConflictMap() + " → " + RotationCalculator.GetNextCrystallineConflictMap());

        ImGui.Separator();
        ImGui.Text("Frontline");
        ImGui.Text(RotationCalculator.GetFrontlineTimeUntilNext().ToString(@"hh\:mm\:ss"));
        ImGui.Text(RotationCalculator.GetCurrentFrontlineMap() + " → " + RotationCalculator.GetNextFrontlineMap());

    }
}
