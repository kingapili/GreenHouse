namespace Model
{
    public interface ISensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public bool IsRunning { get; set; }
        
        /// <summary>
        /// Generates a single random SensorData object valid for given sensor type 
        /// </summary>
        /// <returns>Randomly generated valid SensorData object</returns>
        public SensorData GenerateSingleValue();
    }
}