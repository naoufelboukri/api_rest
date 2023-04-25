using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace quest_web.Models;

public class Address
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MaxLength(100)]
    [Column("street")]
    public string Road { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MaxLength(30)]
    [Column("postal_code")]
    public string PostalCode { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MaxLength(50)]
    [Column("city")]
    public string City { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MaxLength(50)]
    [Column("country")]
    public string Country { get; set; }




    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }





    [DataType(DataType.DateTime)]
    [AllowNull]
    [Column("creation_date", TypeName = "DateTime")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? CreationDate { get; set; }

    [DataType(DataType.DateTime)]

    [AllowNull]
    [Column("updated_date", TypeName = "DateTime")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedDate { get; set; }
}