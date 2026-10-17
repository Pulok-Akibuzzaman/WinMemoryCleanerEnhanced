using System.Collections.Generic;
using System.Linq;

namespace WinMemoryCleaner
{
    /// <summary>  
    /// Log Optimization Data
    /// </summary>  
    public class LogOptimizationData : ILogData, IJsonSerializable
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="LogOptimizationData" /> class.  
        /// </summary>  
        public LogOptimizationData()
        {
            MemoryAreas = new List<LogOptimizationDataMemoryArea>();
        }

        /// <summary>  
        /// Gets or sets the duration.  
        /// </summary>  
        /// <value>  
        /// The duration.  
        /// </value>  
        public string Duration { get; set; }

        /// <summary>  
        /// Gets or sets the duration in milliseconds.  
        /// </summary>  
        /// <value>  
        /// The duration in milliseconds.  
        /// </value>  
        public long DurationMs { get; set; }

        /// <summary>  
        /// Gets or sets the memory.  
        /// </summary>  
        /// <value>  
        /// The memory.  
        /// </value>  
        public List<LogOptimizationDataMemoryArea> MemoryAreas { get; private set; }

        /// <summary>  
        /// Gets or sets the reason.  
        /// </summary>  
        /// <value>  
        /// The reason.  
        /// </value>  
        public string Reason { get; set; }

        /// <summary>  
        /// Gets or sets the invariant reason code.  
        /// </summary>  
        /// <value>  
        /// The invariant reason code.  
        /// </value>  
        public string ReasonCode { get; set; }

        /// <summary>
        /// Converts the log optimization data to a JSON-serializable object.
        /// </summary>
        /// <returns>An anonymous object ready for JSON serialization.</returns>
        public object ToJson()
        {
            var memoryAreas = MemoryAreas
                .OrderBy(m => m.NameCode ?? m.Name)
                .Select(m => string.IsNullOrEmpty(m.Error)
                    ? new { name = m.Name, name_code = m.NameCode, duration = m.Duration, duration_ms = m.DurationMs }
                    : (object)new { name = m.Name, name_code = m.NameCode, duration = m.Duration, duration_ms = m.DurationMs, error = m.Error })
                .ToList();

            return new
            {
                reason = Reason,
                reason_code = ReasonCode,
                duration = Duration,
                duration_ms = DurationMs,
                memoryAreas
            };
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(Localizer.Culture, "{0} ({1}) - {2} area(s)", Reason, Duration, MemoryAreas != null ? MemoryAreas.Count : 0);
        }
    }
}
