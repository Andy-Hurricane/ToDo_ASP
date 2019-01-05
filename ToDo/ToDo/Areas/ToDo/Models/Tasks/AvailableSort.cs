namespace ToDo.Areas.ToDo.Models.Tasks
{
    /// <summary>
    /// Dozwolone sortowania.
    /// </summary>
    public enum AvailableSort
    {
        /// <summary>
        /// Domyślne: ID.
        /// </summary>
        ID,
        /// <summary>
        /// Data rozpoczęcia.
        /// </summary>
        DateStart,
        /// <summary>
        /// Data zakończenia.
        /// </summary>
        DateEnd,
        /// <summary>
        /// Status.
        /// </summary>
        State,
        /// <summary>
        /// Priorytet.
        /// </summary>
        Priority
    }
}