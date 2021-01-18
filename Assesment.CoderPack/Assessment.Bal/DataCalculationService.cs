using Assessment.Abstraction;
using System.Collections.Generic;

namespace Assessment.Bal
{
    /// <summary>
    /// The DataCalculationService
    /// </summary>
    public class DataCalculationService : BaseService, IDataCalculationService
    {
        public IPromotionEngineService PromotionEngineService { get; set; }
        public DataCalculationService()
        {
            this.PromotionEngineService = IoC.Resolve<IPromotionEngineService>();
        }
        public IList<int> GetOrderValue(IList<IList<ScenerioModel>> scenerios)
        {
            return this.PromotionEngineService.GetOrderValue(scenerios);
        }

        public int GetOrderValue(IList<ScenerioModel> scenerio)
        {
            return this.PromotionEngineService.GetOrderValue(scenerio);
        }

    }
}
