using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models
{
	public class Restriction
	{
        //public enum Restrictions
        //{
        //    PublicViewer = 0,
        //    Plus7 = 1,
        //    Plus13 = 2,
        //    Plus18 = 3,
        //    Violance = 4,
        //    Sexuality = 5,
        //    BadHabits = 6
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = "";

        //public Restrictions restrictions { get; set; }
    }
}

