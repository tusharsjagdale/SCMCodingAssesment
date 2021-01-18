namespace Assessment.Bal
{
    using Assessment.Abstraction;
    using Autofac;
    using Promotion.Engine;

    /// <summary>
    /// Defines the <see cref="IoC" />
    /// </summary>
    public static class IoC
    {
        #region Properties 

        private static IContainer container;
        /// <summary>
        /// Gets the Container 
        /// </summary>
        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    var builder = new ContainerBuilder();

                    //update your custom data service here
                    builder.RegisterType<DataBuilderService>().As<IDataBuilderService>().SingleInstance();
                    builder.RegisterType<DataCalculationService>().As<IDataCalculationService>().SingleInstance();

                    //update your custom engine service here
                    builder.RegisterType<PromotionEngineService>().As<IPromotionEngineService>().SingleInstance();

                    container = builder.Build();
                }
                return container;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Resolve
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="T"/></returns>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        #endregion
    }
}
