using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManager.DataEntities.Models
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public int GroupId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Group Name")]
        public string GroupName { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}