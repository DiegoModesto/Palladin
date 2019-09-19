namespace Palladin.Services.LogicService.Options
{
    public static class ErrorsString
    {
        internal const string InvalidToken = "_TOKEN_INVALID_";
        internal const string TokenHasntExpiredYet = "_TOKEN_HASNT_EXPIRED_YET_";
        internal const string UserPasswordNotMatch = "_USR_PWD_NOT_MATCH_";
        internal const string UserHasDeleted = "_USR_IS_DELETED_";
        internal const string UserHasBlocked = "_USR_IS_BLOCKED_";
        internal const string TokenDoesNotExist = "_TOKEN_DOES_NOT_EXIST_";
        internal const string TokenHasExpired = "_TOKEN_HAS_EXPIRED_";
        internal const string TokenInvalidated = "_TOKEN_IS_INVALID_";
        internal const string TokenIsUsed = "_TOKE_HAS_USED_";
        internal const string TokenDoesNotMatchWithJWT = "_TOKEN_DOES_NOT_MATCH_JWT_";
    }
}
