namespace UseAbilities.IoC.Helpers
{
    public class IoCManager
    {
        #region Singleton implementation

        private IoCManager()
        {
            //
        }

        private static readonly Core.IoC ContainerSingleInstance = new Core.IoC();
        public static Core.IoC Container
        {
            get { return ContainerSingleInstance; }
        }

        #endregion
    }
}
