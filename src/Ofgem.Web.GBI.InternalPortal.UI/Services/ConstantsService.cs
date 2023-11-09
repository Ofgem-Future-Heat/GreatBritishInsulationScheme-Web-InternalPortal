namespace Ofgem.Web.GBI.InternalPortal.UI.Services
{
    public static class ConstantsService
    {
		public const string ExternalUsersPage = "/ExternalUsers";
		public const string ExternalUserConfirmPage = $"{ExternalUsersPage}/ExternalUserConfirm";
        public const string ExternalUserSavedPage = $"{ExternalUsersPage}/ExternalUserSaved";
        public const string ExternalUserDeletedPage = $"{ExternalUsersPage}/ExternalUserDeleted";
        public const string ExternalUserEditPage = $"{ExternalUsersPage}/ExternalUserEdit";
        public const string ExternalUserViewPage = $"{ExternalUsersPage}/ExternalUserView";
        public const string ExternalUserAddPage = $"{ExternalUsersPage}/ExternalUserAdd";
		public const string ExternalUserErrorPage = $"{ExternalUsersPage}/ExternalUserErrorPage";

		public const string PersonalDetailsTableTitle = "Personal details";

        public const string ExternalUserListComponent = "ExternalUserList";
        public const string ConfirmUserComponent = "ConfirmUser";
        public const string ViewUserComponent = "ViewUser";
        public const string EditUserComponent = "EditUser";

        public const string TempDataModelState = "ModelState";
        public const string TempDataUserViewModel = "UserViewModel";
    }
}
