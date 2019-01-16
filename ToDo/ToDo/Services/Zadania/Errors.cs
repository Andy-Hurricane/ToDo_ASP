using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.Services.Zadania
{
    public class Errors
    {
        public const string AddEmptyTask = "Nie można dodać pustego zadania.";
        public const string EditFromEmptyTask = "Nie można edytować pustego zadania.";
        public const string EditToEmptyTask = "Nie można edytować na puste zadanie.";
        public const string RemoveEmptyTask = "Nie można usunąć pustego zadania.";

        public const string SwapEmptyTask = "Nie można przesunąć tego zadania, ponieważ nie ma go w bazie.";
        public const string SwapWithEmptyTask = "Nie można przesunąć z tym zadaniem, ponieważ nie ma go w bazie.";

        public const string IdBelowZero = "Zadanie nie może posiadać Id mniejszego od zero.";

    }
}