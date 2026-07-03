namespace PvPTimer;

/// <summary>
/// Provides map rotation calculations for FFXIV PvP modes.
/// Reference dates and intervals sourced from community research.
/// </summary>
public static class RotationCalculator
{
    private const double FrontlineIntervalMinutes = 1440; // 24 hours
    private const double CrystallineConflictIntervalMinutes = 90;

    // The date/time at which each map cycle is known to begin
    private static readonly DateTime FrontlineReferenceDate = new(2025, 7, 16, 15, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime CrystallineConflictReferenceDate = new(2026, 1, 9, 15, 0, 0, DateTimeKind.Utc);

    private static readonly string[] FrontlineMaps =
        { "Secure", "Seize", "Shatter", "Naadam", "Worqor Chirteh" };

    private static readonly string[] CrystallineConflictMaps =
        { "Palaistra", "Volcanic Heart", "Cloud Nine", "Castletown", "Bayside Grappling", "Red Sands" };

    // returns the currently active Frontline map.
    public static string GetCurrentFrontlineMap() =>
        GetMapAtOffset(FrontlineReferenceDate, FrontlineMaps, FrontlineIntervalMinutes, 0);

    // returns the currently active Crystalline Conflict map.
    public static string GetCurrentCrystallineConflictMap() =>
        GetMapAtOffset(CrystallineConflictReferenceDate, CrystallineConflictMaps, CrystallineConflictIntervalMinutes, 0);

    // returns the next Crystalline Conflict map after the current one.
    public static string GetNextCrystallineConflictMap() =>
        GetMapAtOffset(CrystallineConflictReferenceDate, CrystallineConflictMaps, CrystallineConflictIntervalMinutes, 1);

    // returns the next Frontline map after the current one.
    public static string GetNextFrontlineMap() =>
        GetMapAtOffset(FrontlineReferenceDate, FrontlineMaps, FrontlineIntervalMinutes, 1);

    // returns how long until the Frontline map rotates.
    public static TimeSpan GetFrontlineTimeUntilNext() =>
        GetTimeUntilNext(FrontlineReferenceDate, FrontlineIntervalMinutes);

    // returns how long until the Crystalline Conflict map rotates.
    public static TimeSpan GetCrystallineConflictTimeUntilNext() =>
        GetTimeUntilNext(CrystallineConflictReferenceDate, CrystallineConflictIntervalMinutes);

    // returns the map that will be active at a given rotation offset from the current one.
    // an offset of 0 returns the current map, 1 returns the next, and so on.
    private static string GetMapAtOffset(DateTime referenceDate, string[] maps, double intervalMinutes, int offset)
    {
        var elapsed = (DateTime.UtcNow - referenceDate).TotalMinutes;
        var index = ((int)(elapsed / intervalMinutes) + offset) % maps.Length;
        if (index < 0) index += maps.Length;
        return maps[index];
    }

    private static TimeSpan GetTimeUntilNext(DateTime referenceDate, double intervalMinutes)
    {
        var elapsed = (DateTime.UtcNow - referenceDate).TotalMinutes;
        var minutesSinceLast = elapsed % intervalMinutes;
        return TimeSpan.FromMinutes(intervalMinutes - minutesSinceLast);
    }
}
