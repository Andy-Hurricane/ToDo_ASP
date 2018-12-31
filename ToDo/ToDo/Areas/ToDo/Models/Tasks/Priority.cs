namespace ToDo.Models.Tasks
{
    /// <summary>
    /// Dostępne priorytety zadań.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Niezidentyfikowany priorytet.
        /// </summary>
        NULL,
        /// <summary>
        /// Najwyższy priorytet.
        /// </summary>
        MAJOR,
        /// <summary>
        /// Wysoki priorytet.
        /// </summary>
        HIGH,
        /// <summary>
        /// Normalny priorytet.
        /// </summary>
        NORMAL,
        /// <summary>
        /// Niski priorytet.
        /// </summary>
        LOW,
        /// <summary>
        /// Najniższy priorytet.
        /// </summary>
        MINOR
    }
}
