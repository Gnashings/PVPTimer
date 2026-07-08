using System;
namespace PvPTimer;

/// <summary>
/// Provides map rotation calculations for FFXIV PvP modes.
/// Reference dates and intervals sourced from community research. (Thank you pvpc)
/// </summary>
public static class RotationCalculator
{
    private const double FrontlineIntervalMinutes = 1440; // 24 hours
    private const double CrystallineConflictIntervalMinutes = 60;

    // The date/time at which each map cycle is known to begin
    private static readonly DateTime FrontlineReferenceDate = new(2026, 7, 3, 15, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime CrystallineConflictReferenceDate = new(2026, 7, 3, 18, 0, 0, DateTimeKind.Utc);

    private static readonly string[] FrontlineMaps =
        { "Triumph", "Seize", "Secure", "Naadam", "Triumphs", "Seize", "Shatter", "Naadam" };

    private static readonly string[] CrystallineConflictMaps =
        { "Palaistra", "Volcanic Heart", "Bayside Battleground", "Cloud Nine",
      "Clockwork Castletown", "Archeia Harmonias", "Red Sands" };

    public static string GetCurrentFrontlineMap(DateTime? now = null) =>
        GetMapAtOffset(FrontlineReferenceDate, FrontlineMaps, FrontlineIntervalMinutes, 0, now ?? DateTime.UtcNow);

    public static string GetCurrentCrystallineConflictMap(DateTime? now = null) =>
        GetMapAtOffset(CrystallineConflictReferenceDate, CrystallineConflictMaps, CrystallineConflictIntervalMinutes, 0, now ?? DateTime.UtcNow);

    public static string GetNextCrystallineConflictMap(DateTime? now = null) =>
        GetMapAtOffset(CrystallineConflictReferenceDate, CrystallineConflictMaps, CrystallineConflictIntervalMinutes, 1, now ?? DateTime.UtcNow);

    public static string GetNextFrontlineMap(DateTime? now = null) =>
        GetMapAtOffset(FrontlineReferenceDate, FrontlineMaps, FrontlineIntervalMinutes, 1, now ?? DateTime.UtcNow);

    public static TimeSpan GetFrontlineTimeUntilNext(DateTime? now = null) =>
        GetTimeUntilNext(FrontlineReferenceDate, FrontlineIntervalMinutes, now ?? DateTime.UtcNow);

    public static TimeSpan GetCrystallineConflictTimeUntilNext(DateTime? now = null) =>
        GetTimeUntilNext(CrystallineConflictReferenceDate, CrystallineConflictIntervalMinutes, now ?? DateTime.UtcNow);

    private static string GetMapAtOffset(DateTime referenceDate, string[] maps, double intervalMinutes, int offset, DateTime now)
    {
        var elapsed = (now - referenceDate).TotalMinutes;
        var index = ((int)(elapsed / intervalMinutes) + offset) % maps.Length;
        if (index < 0) index += maps.Length;
        return maps[index];
    }

    private static TimeSpan GetTimeUntilNext(DateTime referenceDate, double intervalMinutes, DateTime now)
    {
        var elapsed = (now - referenceDate).TotalMinutes;
        var remaining = intervalMinutes - (elapsed % intervalMinutes);
        return TimeSpan.FromMinutes(remaining % intervalMinutes == 0 ? intervalMinutes : remaining);
    }
}