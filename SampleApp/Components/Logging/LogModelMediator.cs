﻿using System.ComponentModel;
using System.Linq;

using WPFUtilities.Components.Logging.ListLogger;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// log view model - model mediator
    /// </summary>
    public class LogModelMediator
    {
        /// <summary>
        /// build a new instance
        /// </summary>
        /// <param name="logViewModel">log view model</param>
        /// <param name="listLoggerModel">list logger model</param>
        public LogModelMediator(
            ILogViewModel logViewModel,
            IListLoggerModel listLoggerModel)
        {
            logViewModel.Messages = new BindingList<string>(
                listLoggerModel.LogItems.ToList());

            void LogItems_ListChanged(object sender, ListChangedEventArgs e)
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.Reset:
                        logViewModel.Messages.Clear();
                        break;
                    case ListChangedType.ItemAdded:
                        logViewModel.Messages.Add(listLoggerModel.LogItems[e.NewIndex]);
                        break;
                }
            }

            listLoggerModel.LogItems.ListChanged += LogItems_ListChanged;
        }
    }
}