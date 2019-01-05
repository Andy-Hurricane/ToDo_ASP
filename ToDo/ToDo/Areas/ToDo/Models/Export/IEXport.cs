using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.Areas.ToDo.Models.Export
{
    public interface IExport
    {
        string ContentType { get; }

        /// <summary>
        /// Funkcja przygotowująca dane do eksportu.
        /// </summary>
        /// <param name="actualSite">Jeśli wynosi "on", to pobierze tylko aktualną stronę/</param>
        /// <returns>Prawdę dla poprawnego przygotowania danych.</returns>
        bool PrepareData(string actualSite);

        /// <summary>
        /// Eksportuje dane.
        /// </summary>
        /// <returns>Zwraca plik w formacie string.</returns>
        string Export();

        /// <summary>
        /// Zwraca długość pliku eksportowanego.
        /// </summary>
        /// <returns>Długość pliku.</returns>
        int Legnth();
    }
}