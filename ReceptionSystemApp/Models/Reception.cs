using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReceptionSystemApp.Models
{
        public class Reception
        {
            [Key]
            public int Id { get; set; }

            public int? VisitorId { get; set; }
            public Visitor? Visitor { get; set; }

            public DateTime Date { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public Status Status { get; set; }
        }

        public enum Status
        {
            Pending,
            Confirmed,
            Completed,
            Cancelled
        }
    }