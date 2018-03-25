public static class Names {
    public const string SONARCUBE_API_TOKEN = "CI_CONAR_TOKEN";
    public const string SONARQUBE_URI = "CI_SONAR_URL";
    public const string NUGET_URI = "NUGET_REPO_URL";
    public const string NUGET_API_TOKEN = "NUGET_API_KEY";

    public const string PROJECT_ID = "WD.Translations";
    public const string PROJECT_ID_ABSTRACTIONS = "WD.Translations.Abstractions";

    public const string PROJECT_TITLE = "WebDucer translation library";
    public const string PROJECT_TITLE_ABSTRACTIONS = "WebDucer abstractions for translation library";
    public static readonly string[] PROJECT_AUTHORS = {"Eugen [WebDucer] Richter"};
    public static readonly string[] PROJECT_OWNERS = {"Eugen [WebDucer] Richter"};
    public const string PROJECT_DESCRIPTION = @"WebDucer library for handle translation in Xamarin.Forms in XAML and view models";
    public const string PROJECT_DESCRIPTION_ABSTRACTIONS = @"WebDucer abstractions (ITranslationServie and IResourceManagerSource)";
    public static readonly string PROJECT_COPYRIGHTS = string.Format("MIT - (c) {0} Eugen [WebDucer] Richter", DateTime.Now.Year);
    public static readonly string[] PROJECT_TAGS = {"XAML", "Xamarin", "Xamarin.Forms", "WebDucer", "Forms", "MarkupExtension", "Translation", "Localization", "l18n", "i18n"};

    public const string SONARQUBE_ORGANISATION = "webducer-oss";
}