using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Indian
{
    public class Issue
    {
        //ISSUE-begins with date,comments begins with 5,alert 4,information-6
        //iID"091512"+"todays count"=0915121,0915122,0915123... issues-complaints  NV(13)

        //issue-type : COMPLAIN-0,ALERT-9,INFORMATION-6 ,COMMENTS-5
        //(5)comments---for 0915122 is--->5-0915122-1 ,5-0915122-2,5-0915122-3 NV(14)
        //(9)alert,emmergency--- 9-091512-1,9-091512-2 
        //(6)information - 6-091512-1,6-091512-2
        //attachemnts P=iid+"count" eg=for 09151222 issue,ll be 09151222-1/2/3....in different tbl
        [Key]
        public string IssueID { get; set; }// get{return IssueID;} set{value = "" + DateTime.Today.Date.ToString("Myd")+"";} } //{ return IssueID; } set { value = "" + DateTime.Today.Date.ToString("Myd")+""; } } //ISSUE-ID

        [Required(ErrorMessage = "You must enter Subject")]
        [StringLength(222, MinimumLength = 10, ErrorMessage = "Subject should be of standard length.")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }//restrict to 255char

        [Required(ErrorMessage = "You must enter Detail of Issue")]
        [StringLength(3333, MinimumLength = 33, ErrorMessage = "Description should be of standard length.")]
        [Display(Name = "Description of Issue")]
        public string Description { get; set; }//restrict to 55 to 5555 char

        [Display(Name = "You Want to display this issue to public")]
        public bool IssueVisibility { get; set; }         //visibility to public

        [Display(Name = "You Want to display your name to public")]
        public bool CreatorVisibility { get; set; }         //visibility to public

        [Display(Name = "Select Issue Category")]
        public byte CategoryID { get; set; }//category id

        [ScaffoldColumn(false)]
        public virtual string CreatorID { get; set; }//guest or creator id

        //public Int16 pincode { get; set; }
        //public virtual Place place{ get; set; } //pincode1_pincode2

        public string comment_id { get; set; }
        public virtual ICollection<Comment> Comments{ get; set; } 

        public string attachment_id { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }

        public DateTime creation_date { get; set; }
        //[Display(Name = "Select your Region")]
        //[StringLength(8, MinimumLength = 2, ErrorMessage = "Enter Valid Pincode")]
        //public virtual string PlaceID { get; set; } //pincode
        //eg for iid=091512-22 ie=09151268,here 68 is count of that day...it may be 9875355 also
        //[ScaffoldColumn(false)]
        //public virtual string AttachmentIDs { get; set; }//DIFFERENT TBL//eg aid=091512683-091512681-0915122278
        //[ScaffoldColumn(false)]
        //public virtual string CommentIDs { get; set; }//eg cid=5091512681 or 50915126843 or 5091512681-50915126843
        //[ScaffoldColumn(false)]
        //public virtual string SupportIDs { get; set; }//eg(group of creator ids) eg: crid-crid-crid

        //[ScaffoldColumn(false)]
        //public virtual string OpposeIDs { get; set; }//eg(group of creator ids) eg: crid-crid-crid

        //[ScaffoldColumn(false)]
        //public virtual Attachment Attachment { get; set; }
    }


    //public class Place
    //{
    //    [Key]
    //    [Range(01, 9999999)]
    //    public Int16 pincode { get; set; }
    //    public string place_name { get; set; }
    //    public string district { get; set; }
    //    public string state { get; set; }

    //    public virtual Issue issues{get;set;}
    //}
    //public class user
    //{
    //    public string user_name { get; set; }
    //    public string contact_no { get; set; }
    //    public EmailAddressAttribute email { get; set; }
    //    public string uid { get; set; }
    //    public string pan_no { get; set; }
    //}
    public class Comment
    {
        [Key]
        public string comment_id{get;set;}
        public string description { get; set; }
        public virtual ICollection<Issue> issues { get; set; }
    }
    public class Attachment //Attachemnts
    {
        [Key]
        public string AttachmentID { get; set; }
        public string IssueID { get; set; }
        public virtual ICollection<Issue> issues { get; set; }
        //   public virtual HttpFileCollection  file { get; set; }
    }

    public class IssuesCount  //issues-count-per day
    {
        //public IssuesCount(DateTime dt,int count=0)
        //{ date = DateTime.Today.Date;
        //IssuesCountDay = count;
        //}
        [Key]
        public DateTime date { get; set; }
        public int IssuesCountDay { get; set; }
    }
    public class IndianDbContext : DbContext
    {
        public DbSet<Issue> Db_Issues { get; set; }
        public DbSet<Comment> Db_Comments { get; set; }
        //public DbSet<Place> Db_Places { get; set; }
        public DbSet<Attachment> Db_Attachements { get; set; }
        public DbSet<IssuesCount> Db_IssuesCounts { get; set; }
        // public DbSet<Category> _categoryDbContect {get; set;}
        //following is to avoid pluralizing the table names like Issues,Places
        //protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }
}
