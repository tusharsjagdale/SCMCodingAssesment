using System;
using System.Collections.Generic;

namespace Assessment.Abstraction
{
    /// <summary>
    /// The IDataBuilderService
    /// </summary>
    public interface IDataBuilderService : IDisposable
    {
        /// <summary>
        /// The GetSKUIdPrices
        /// </summary>
        /// <returns>IDictionary<SKUId, int></returns>
        IDictionary<SKUId, int>  GetSKUIdPrices();

        /// <summary>
        /// The GetActivePromotions
        /// </summary>
        /// <returns>IDictionary<SKUId, int></returns>
        IDictionary<PromotionType, Dictionary<SKUId, PromotionValueModel>> GetActivePromotions();

        /// <summary>
        /// The GetCalculationScenerios
        /// </summary>
        /// <returns>IList<IList<ScenerioModel>></returns>
        IList<IList<ScenerioModel>> GetCalculationScenerios();
    }
}
