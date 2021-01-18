using Assessment.Abstraction;
using Assessment.Bal;
using System;

namespace Assesment.CoderPack
{
    class Program
    {
        static void Main(string[] args)
        {
            //prepare data from data builder service
            var dataBuilderService =  IoC.Resolve<IDataBuilderService>();
            Constants.UnitPrices = dataBuilderService.GetSKUIdPrices();
            Constants.ActivePromotions = dataBuilderService.GetActivePromotions();

            //get the scenerios to validate
            var sceneriosToValidate = dataBuilderService.GetCalculationScenerios();

            //get the output from data calculation service
            var dataCalculationService =  IoC.Resolve<IDataCalculationService>();
            var output = dataCalculationService.GetOrderValue(sceneriosToValidate);

            //print the output
            var scenerioNumber = 1;
            Console.WriteLine("************* Problem Statement 1: Promotion Engine ****************************");
            Console.WriteLine("");
            Console.WriteLine("************* OUTPUT START *****************************************************");
            Console.WriteLine("");
            foreach (var item in output)
                Console.WriteLine(string.Format("Output of scenerio {0} is => {1}", scenerioNumber++, item));
            Console.WriteLine("");
            Console.WriteLine("************* OUTPUT END *******************************************************");

            Console.WriteLine("");
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
