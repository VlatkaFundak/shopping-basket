﻿namespace ShoppingBasket.Service.Infrastructure
{
    public interface ILogger
    {
        void LogInfo(string message);

        void LogError(string message, Exception ex);
    }
}