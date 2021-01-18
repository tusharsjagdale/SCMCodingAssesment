using Assessment.Abstraction;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Promotion.Engine
{
    /// <summary>
    /// The PromotionEngineService
    /// </summary>
    public class PromotionEngineService : BaseService, IPromotionEngineService
    {
        /// <summary>
        /// The GetOrderValue
        /// </summary>
        /// <param name="scenerios">scenerios</param>
        /// <returns></returns>
        public IList<int> GetOrderValue(IList<IList<ScenerioModel>> scenerios)
        {
            IList<int> values = new List<int>();
            foreach (var scenerio in scenerios)
                values.Add(this.GetOrderValue(scenerio));
            return values;
        }

        /// <summary>
        /// The GetOrderValue
        /// </summary>
        /// <param name="scenerio">scenerio</param>
        /// <returns></returns>
        public int GetOrderValue(IList<ScenerioModel> scenerio)
        {
            /*Assumption => there can be multiple sku id in single scenerios
             *  so find out the total unit of individual sku id*/
            IList<ScenerioModel> uniqueSkuIdUnit = new List<ScenerioModel>();
            foreach (var item in scenerio)
                if (uniqueSkuIdUnit.FirstOrDefault(s => s.SKUId == item.SKUId) == null)
                    uniqueSkuIdUnit.Add(new ScenerioModel()
                    {
                        SKUId = item.SKUId,
                        Unit = scenerio.Where(s => s.SKUId == item.SKUId).Sum(s => s.Unit)
                    });

            //ignore the items whose unit is 0
            uniqueSkuIdUnit = uniqueSkuIdUnit.Where(s => s.Unit > 0).ToList();

            var value = 0;
            foreach (var item in uniqueSkuIdUnit)
            {
                //check whether the unit price is available or not - if not available then unit price assumed to be zero
                if (Constants.UnitPrices.ContainsKey(item.SKUId) && !item.IsHandled)
                {
                    var result = this.CalculateValue(ref uniqueSkuIdUnit, item);
                    value = value + result;
                }
                else
                {
                    //no unit price available so siple ignore it or its value can be 0
                }
            }
            return value;
        }

        /// <summary>
        /// The CalculateValue
        /// </summary>
        /// <param name="scenerios">scenerios</param>
        /// <param name="item">item</param>
        /// <returns></returns>
        private int CalculateValue(ref IList<ScenerioModel> scenerios, ScenerioModel item)
        {
            var sku = item.SKUId;
            var skuUnit = item.Unit;
            var skuUnitValue = Constants.UnitPrices[item.SKUId];
            var skuValue = 0;

            //get the available Promotions for sku
            var skuAvailablePromotions = Constants.ActivePromotions.Where(s => s.Value.ContainsKey(sku));
            if (!skuAvailablePromotions.Any())
            {
                //no any Promotion available for this sku id so calculate the value according to unit price
                skuValue = skuUnit * skuUnitValue;
            }
            else
            {
                //Promotions are availble
                //as per given assumtions - Promotions is mutually exclusive so only one can be taken
                var availablePromotionType = skuAvailablePromotions.FirstOrDefault().Key;
                var availablePromotion = skuAvailablePromotions.FirstOrDefault().Value[sku];

                switch (availablePromotionType)
                {
                    case PromotionType.NItemOfSkuId:
                        skuValue = this.HandleNUnitPromotionType(item, availablePromotion);
                        break;
                    case PromotionType.AndOfTwoSkuId:
                        skuValue = this.HandleAndOfTwpSkuPromotionType(ref scenerios, ref skuUnit, skuUnitValue, availablePromotion);
                        break;
                    case PromotionType.XPercentageOfUnitPrice:
                        Debugger.Break();
                        //new Promotion type logic goes here
                        break;
                    default:
                        break;
                }

            }
            item.IsHandled = true;
            return skuValue;
        }

        /// <summary>
        /// The HandleAndOfTwpSkuPromotionType
        /// </summary>
        /// <param name="scenerios"></param>
        /// <param name="skuUnit"></param>
        /// <param name="skuUnitValue"></param>
        /// <param name="availablePromotion"></param>
        /// <returns></returns>
        private int HandleAndOfTwpSkuPromotionType(ref IList<ScenerioModel> scenerios, ref int skuUnit, int skuUnitValue, PromotionValueModel availablePromotion)
        {
            int skuValue;
            var partnerSku = availablePromotion.SKUId;

            HandleMainSku:
            //check whether any scenerios items is there in which sku partner is available and which is not handled yet
            var partnerSkuScenerios = scenerios.FirstOrDefault(s => !s.IsHandled && s.SKUId == partnerSku && s.Unit > 0);
            if (partnerSkuScenerios != null)
            {
                var partnerSkuValue = availablePromotion.Value;

                //take only one value of self
                skuValue = 1 * partnerSkuValue;

                //take only one value of partner sku
                if (partnerSkuScenerios.Unit == 1)
                    partnerSkuScenerios.IsHandled = true;
                else
                    partnerSkuScenerios.Unit = partnerSkuScenerios.Unit - 1;

                //in case given unit value is greater than 1
                skuUnit = skuUnit - 1;
                if (skuUnit > 1)
                {
                    goto HandleMainSku;
                }
            }
            else
            {
                //partners data is not available so calculate the value according to number of unit
                skuValue = skuUnit * skuUnitValue;
            }

            return skuValue;
        }

        private int HandleNUnitPromotionType(ScenerioModel item, PromotionValueModel availablePromotion)
        {
            var skuUnit = item.Unit;
            var skuUnitValue = Constants.UnitPrices[item.SKUId];

            int skuValue;
            var nUnitOfSku = availablePromotion.Unit;

            if (skuUnit >= nUnitOfSku)
            {
                var valueOdnUnitOfSku = availablePromotion.Value;

                var averageNoOfnUnitOfSku = skuUnit / nUnitOfSku;

                //add Promotions unit value
                skuValue = averageNoOfnUnitOfSku * valueOdnUnitOfSku;

                //add remaning unit value
                var remainingUnit = skuUnit - (averageNoOfnUnitOfSku * nUnitOfSku);
                if (remainingUnit > 0)
                    skuValue = skuValue + (remainingUnit * skuUnitValue);
            }
            else
            {
                //given unit is less that the Promotion unit
                skuValue = skuUnit * skuUnitValue;
            }

            return skuValue;
        }
    }
}
