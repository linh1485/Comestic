﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace demo2_mp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.OrderProes = new HashSet<OrderPro>();
        }
    
        public int ID { get; set; }
        [Required(ErrorMessage = "UserName không được rỗng")]
        [Display(Name = "UserName")]

        public string NameUser { get; set; }
        [Required(ErrorMessage = "Password không được rỗng")]
        [Display(Name = "Password")]

        public string PasswordUser { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được rỗng")]
        [Display(Name = "PhoneNumber")]

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Họ tên không được rỗng")]
        [Display(Name = "FullName")]

        public string FullName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string DiaChi { get; set; }
        public string Img { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPro> OrderProes { get; set; }
    }
}
