namespace Model
{
    public interface ISensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public bool IsRunning { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        
        /// <summary>
        /// Generates a single random SensorData object valid for given sensor type 
        /// </summary>
        /// <returns>Randomly generated valid SensorData object</returns>
        public SensorData GenerateSingleValue();
        
        /// <summary>
        /// Generates a single random SensorData object valid for given sensor type 
        /// </summary>
        /// <param name="value">Value to be generated</param>
        /// <returns>Randomly generated valid SensorData object</returns>
        public SensorData GenerateSingleValue(double value);
    }
}