using System.Collections.Generic;

namespace Assessment.Abstraction
{
    /// <summary>
    /// The Constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The UnitPrices
        /// </summary>
        public static IDictionary<SKUId, int> UnitPrices { get; set; }

        /// <summary>
        /// The ActivePromotions
        /// </summary>
        public static IDictionary<PromotionType, Dictionary<SKUId, PromotionValueModel>> ActivePromotions { get; set; }
    }
}
