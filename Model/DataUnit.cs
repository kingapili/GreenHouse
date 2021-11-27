namespace Model
{
    /// <summary>
    /// Includes all possible data units for available sensor types:
    /// WattsPerMeterSquared   -> solar radiation sensors,
    /// Percent                -> humidity sensors,
    /// Ppm (parts per milion) -> CO2 sensors,
    /// Degrees Celsius        -> temperature sensors
    /// </summary>
    public enum DataUnit
    {
        WattsPerMeterSquared,
        Percent,
        Ppm,
        DegreesCelsius
    }
}