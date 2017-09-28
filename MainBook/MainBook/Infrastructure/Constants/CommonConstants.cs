namespace MainBook.Infrastructure.Constants
{
    public class CommonConstants
    {
    #if DEBUG
            public const string AdControl_UnitId = "test";
            public const string AdControl_ApplicationId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1";
    #else
            public const string AdControl_UnitId = "11700626";
            public const string AdControl_ApplicationId = "9ppnv3k7652l";
    #endif
    }
}
