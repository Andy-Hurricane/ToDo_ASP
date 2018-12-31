namespace ToDo.Areas.ToDo.Models.Tasks
{
    /// <summary>
    /// Status zadania.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Zadanie otwarte.
        /// </summary>
        OPEN,
        /// <summary>
        /// Zadanie w trakcie realizacji.
        /// </summary>
        WORK_IN_PROGRESS,
        /// <summary>
        /// Zadanie zamknięte.
        /// </summary>
        CLOSE
    }
}
