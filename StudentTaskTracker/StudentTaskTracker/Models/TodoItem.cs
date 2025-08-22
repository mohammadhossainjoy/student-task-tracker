﻿using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTaskTracker.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Notes { get; set; }

        public bool IsDone { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
