﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KH.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(PreStartApp), "Start")]
namespace KH.App_Start
{
    public static class PreStartApp
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Метод запускается один раз перед стартом приложения        
        /// </summary>
        public static void Start()
        {
            logger.Info("Application PreStart");
        }
    }
}