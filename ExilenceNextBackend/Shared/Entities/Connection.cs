﻿namespace Shared.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Connection
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }

        [Required] [StringLength(100)] public string ConnectionId { get; set; }

        [Required] [StringLength(20)] public string InstanceName { get; set; }

        [Required] [StringLength(20)] public DateTime Created { get; set; }

        public virtual Account Account { get; set; }
        public virtual Group Group { get; set; }
    }
}
