using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payra.DataManager.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>, ICommonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? UserTypeId { get; set; }
        public int? UserSubTypeId { get; set; }
        public bool IsFirstLogin { get; set; }
        // interface propertise
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ChangePasswordDate { get; set; }
        public bool? IsUserLocked { get; set; }
    }
    public class ApplicationRole : IdentityRole<int>, ICommonModel
    {
        // interface propertise
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UsmUserRole : IdentityUserRole<int>, ICommonModel
    {
        [NotMapped]
        public ApplicationRole ApplicationRole { get; set; }
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    [Serializable]
    public class Menu : ICommonModel
    {

        public int Id { get; set; }
        public int Parent_Id { get; set; }
        public bool IsChild { get; set; }
        public string key { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? MenuOrder { get; set; }
        public string MenuIcon { get; set; }
        [NotMapped]
        public string ParentMenuName { get; set; }
        // Common field

        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class ControllerActionMapping : ICommonModel
    {
        public int Id { get; set; }

        public int MenuId { get; set; }
        public string ControllerName { get; set; }
        public string ControllerToView { get; set; }
        public string ActionName { get; set; }
        public string ActionToView { get; set; }

        //public bool IsSelected { get; set; }
        // public DateTime CreatedDate { get; set; }
        // Common field
        public bool IsUrlAction { get; set; }
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
        public Menu Menu { get; set; }

        [NotMapped]
        public int? RoleId { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
        [NotMapped]
        public bool Permission { get; set; }

    }

    public class RolePermission //: ICommonModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ControllerActionId { get; set; }
        public bool Permission { get; set; }

        //public bool IsActive { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        // public bool IsDeleted { get; set; }

        
    }
    public interface ICommonModel
    {
        bool IsActive { get; set; }
        int EntryBy { get; set; }
        DateTime EntryDate { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        bool IsDeleted { get; set; }
    }

}
