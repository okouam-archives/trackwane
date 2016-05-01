namespace Trackwane.AccessControl.Engine.Services
{
    public struct Message
    {
        public static string ROOT_ATTEMPT_WHEN_EXISTING_USERS = "The root user cannot be created if other users already exist";

        public const string UNKNOWN_USER = "The user with ID <{0}> could not be found";
        public const string UNKNOWN_ORGANIZATION = "The organization with ID <{0}> could not be found";

        public const string DUPLICATE_ORGANIZATION_NAME = "The name <{0}> is already in use by another organization";
        public const string DUPLICATE_ORGANIZATION_ID = "The ID <{0}> is already in use by another organization";
        public const string DUPLICATE_USER_ID = "The ID <{0}> is already in use by another organization";
        public const string DUPLICATE_USER_EMAIL = "The email <{0}> is already in use by another user";

        public const string INVALID_PASSWORD = "The password provided is invalid";
        public const string INVALID_CREDENTIALS = "The credentials provided are invalid";
    }
}
