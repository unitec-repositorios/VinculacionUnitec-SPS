using HoursTracker.Domain.Aggregates.Students;
using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace HoursTracker.Domain.Aggregates.DataBot
{
    public class DataBotS : BaseEntity, IAggregateRoot
    {
        public string Telegramid { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
        public int Verified { get; set; }
        public DateTime Fecha_confirmacion { get; set; }
        public int Estado { get; set; }
        public DateTime Fecha_UltimoToken { get; set; }
        public int StudentRef { get; set; }
        public Student Student { get; set; }
    }
}
