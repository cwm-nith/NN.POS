using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NN.POS.API.Infra.Tables.Roles;

[Table("roles")]
public class RoleTable : BaseTable
{
    [Column("name"), StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Column("display_name"), StringLength(150)]
    public string? DisplayName { get; set; }

    [Column("description"), StringLength(250)]
    public string? Description { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

}