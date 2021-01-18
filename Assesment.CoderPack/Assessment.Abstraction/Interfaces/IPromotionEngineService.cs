﻿using System;
using System.Collections.Generic;

namespace Assessment.Abstraction
{
    /// <summary>
    /// The IPromotionEngineService
    /// </summary>
    public interface IPromotionEngineService : IDisposable
    {
        /// <summary>
        /// The GetOrderValue
        /// </summary>
        /// <param name="scenerios">scenerios</param>
        /// <returns></returns>
        IList<int> GetOrderValue(IList<IList<ScenerioModel>> scenerios);

        /// <summary>
        /// The GetOrderValue
        /// </summary>
        /// <param name="scenerio">scenerio</param>
        /// <returns></returns>
        int GetOrderValue(IList<ScenerioModel> scenerio);
    }
}
