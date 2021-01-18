using Assessment.Abstraction;
using System.Collections.Generic;

namespace Assessment.Bal
{
    /// <summary>
    /// The DataBuilderService
    /// </summary>
    public class DataBuilderService : BaseService, IDataBuilderService
    {
        /// <summary>
        /// The GetSKUIdPrices
        /// </summary>
        /// <returns>IDictionary<SKUId, int></returns>
        public IDictionary<SKUId, int> GetSKUIdPrices()
        {
            /*
             * data can be fetched from database or weservice 
             * for now we are using the data given in assignment itself
             * we can add more data from here
             * We are adding given sample unit prices here
             */
            IDictionary<SKUId, int> unitPrices = new Dictionary<SKUId, int>();

            unitPrices.Add(SKUId.A, 50);
            unitPrices.Add(SKUId.B, 30);
            unitPrices.Add(SKUId.C, 20);
            unitPrices.Add(SKUId.D, 15);

            return unitPrices;
        }

        /// <summary>
        /// The GetActivePromotions
        /// </summary>
        /// <returns>IDictionary<PromotionType, Dictionary<SKUId, PromotionValueModel>></returns>
        public IDictionary<PromotionType, Dictionary<SKUId, PromotionValueModel>> GetActivePromotions()
        {
            /*
             * data can be fetched from database or weservice 
             * for now we are using the data given in assignment itself
             * we can add more data from here
             * we are adding given sample Promotion types here
             */
            IDictionary<PromotionType, Dictionary<SKUId, PromotionValueModel>> activePromotions = 
                new Dictionary<PromotionType, Dictionary<SKUId, PromotionValueModel>>();

            activePromotions.Add(PromotionType.NItemOfSkuId, new Dictionary<SKUId, PromotionValueModel>() {
                { SKUId.A,new PromotionValueModel(){ Unit=3,Value=130 } },
                { SKUId.B,new PromotionValueModel(){ Unit=2,Value=45 } }
            });

            activePromotions.Add(PromotionType.AndOfTwoSkuId, new Dictionary<SKUId, PromotionValueModel>() {
                { SKUId.C,new PromotionValueModel(){ SKUId=SKUId.D ,Value=30 } }
            });

            return activePromotions;
        }

        /// <summary>
        /// The GetCalculationScenerios
        /// </summary>
        /// <returns>IDictionary<SKUId, int></returns>
        public IList<IList<ScenerioModel>> GetCalculationScenerios()
        {
            /*
             * data can be fetched from database or weservice 
             * for now we are using the data given in assignment itself
             * we can add more data from here
             * we are adding given sample scenerios here
             */
            IList<IList<ScenerioModel>> scenerios = new List<IList<ScenerioModel>>();

            //Scenerio A
            scenerios.Add(new List<ScenerioModel> {
                new ScenerioModel(){ Unit=1, SKUId= SKUId.A },
                new ScenerioModel(){ Unit=1, SKUId= SKUId.B },
                new ScenerioModel(){ Unit=1, SKUId= SKUId.C },
            });

            //Scenerio B
            scenerios.Add(new List<ScenerioModel> {
                new ScenerioModel(){ Unit=5, SKUId= SKUId.A },
                new ScenerioModel(){ Unit=5, SKUId= SKUId.B },
                new ScenerioModel(){ Unit=1, SKUId= SKUId.C },
            });

            //Scenerio C
            scenerios.Add(new List<ScenerioModel> {
                new ScenerioModel(){ Unit=3, SKUId= SKUId.A },
                new ScenerioModel(){ Unit=5, SKUId= SKUId.B },
                new ScenerioModel(){ Unit=1, SKUId= SKUId.C },
                new ScenerioModel(){ Unit=1, SKUId= SKUId.D },
            });

            return scenerios;
        }
    }
}
