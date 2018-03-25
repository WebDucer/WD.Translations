using WD.Translations;

namespace Translations.Tests.Mocks
{
    public class TestResourceManagersSource : ResourceManagersSource
    {
        public static void Reset()
        {
            Current = null;
        }
    }
}