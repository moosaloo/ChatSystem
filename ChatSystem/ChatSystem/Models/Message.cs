using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ChatSystem.Models
{
    /// <summary>
    /// کلاس پیغام 
    /// </summary>
    public class Message
    {
        public virtual int Id { get; set; }
        public virtual string Content { get; set; }
        public virtual string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }
        public virtual DateTime SendDateTime { get; set; }
        public virtual string RoomName { get; set; }
        [ForeignKey("RoomName")]
        public virtual Room Room { get; set; }
    }
}
