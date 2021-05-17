﻿namespace Gravitas.Platform.Web.ViewModel
{
    public class EmployeeItemVm : BaseEntityVm<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public bool IsFolder { get; set; }
    }
}