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
            MinimumSize = new Vector2(300, 175),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.Separator();

        ImGui.Text("Crystalline Conflict");

        ImGui.Text(RotationCalculator.GetCrystallineConflictTimeUntilNext().ToString(@"hh\:mm\:ss"));

        ImGui.TextColored(new Vector4(0, 1, 0, 1), RotationCalculator.GetCurrentCrystallineConflictMap());
        ImGui.SameLine();
        ImGui.Text(" → " + RotationCalculator.GetNextCrystallineConflictMap());

        ImGui.Separator();

        ImGui.Text("Frontline");

        ImGui.Text(RotationCalculator.GetFrontlineTimeUntilNext().ToString(@"hh\:mm\:ss"));
        ImGui.TextColored(new Vector4(0, 1, 0, 1), RotationCalculator.GetCurrentFrontlineMap());
        ImGui.SameLine();
        ImGui.Text(" → " + RotationCalculator.GetNextFrontlineMap());



    }
}
