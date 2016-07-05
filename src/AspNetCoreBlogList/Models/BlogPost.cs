using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreBlogList.Models
{
    public class BlogPost
    {
        [Key]
        [ScaffoldColumn(false)]
        public int BlogPostId { get; set; }

        [Display(Name = "Blog Post Title")]
        [Required]
        public string BlogPostTitle { get; set; }

        [Display(Name = "Featured Image")]
        public byte[] BlogPostImage { get; set; }

        [Display(Name = "Author")]
        public string BlogAuthor { get; set; }

        [RegularExpression(@"^@?(\w){1,15}$", ErrorMessage = "Enter a valid user name")]
        [Display(Name = "Twitter Handle")]
        public string TwitterHandle { get; set; }

        [Display(Name = "Blog Post Link")]
        [Required]
        public string BlogPostLink { get; set; }

        [Display(Name = "Version")]
        [Required]
        public AspNetCoreVersion AspNetCoreVersion { get; set;}
    }

    public enum AspNetCoreVersion
    {
        ASPNETCoreRTM,
        ASPNETCoreRC2,
        ASPNETCoreRC1,
        AllVersions
    }
}
