using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDo.Areas.ToDo.Models.Export
{
    public interface IExport
    {
        /// <summary>
        /// Zwraca nazwę pliku.
        /// </summary>
        string GetName { get; }

        /// <summary>
        /// Ustawienia content-type dla pliku.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Zwraca długość pliku eksportowanego.
        /// </summary>
        /// <returns>Długość pliku.</returns>
        int Length { get; }

        /// <summary>
        /// Rozszerzenie pliku.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Funkcja przygotowująca dane do eksportu.
        /// </summary>
        /// <param name="actualSite">Jeśli wynosi "on", to pobierze tylko aktualną stronę/</param>
        /// <returns>Prawdę dla poprawnego przygotowania danych.</returns>
        bool PrepareData(bool actualSite);

        /// <summary>
        /// Eksportuje dane.
        /// </summary>
        /// <returns>Zwraca plik w formacie string.</returns>
        void Export();
    }
}