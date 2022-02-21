namespace PLS.Application.Common.Logging
{
    public enum LogLevel
    {
        Verbose = 0,
        //
        // Summary:
        //     Internal system events that aren't necessarily observable from the outside.
        Debug = 1,
        //
        // Summary:
        //     The lifeblood of operational intelligence - things happen.
        Info= 2,
        //
        // Summary:
        //     Service is degraded or endangered.
        Warn= 3,
        //
        // Summary:
        //     Functionality is unavailable, invariants are broken or data is lost.
        Error = 4,
        //
        // Summary:
        //     If you have a pager, it goes off when one of these occurs.
        Fatal = 5
    }
}
