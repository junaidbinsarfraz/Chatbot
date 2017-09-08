using Chatbot.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Utils
{
    public static class ChatUtil
    {

        public static ConcurrentDictionary<string, UserConnection> getUserConnectionByUserId(ConcurrentDictionary<string, UserConnection> userConnections, long userId)
        {
            ConcurrentDictionary<string, UserConnection> selectedUserConnections = new ConcurrentDictionary<string, UserConnection>();

            // Filter userConnections
            foreach(var item in userConnections)
            {
                // Check if user 
                if (item.Value != null && item.Value.User != null && item.Value.User.Id == userId)
                {
                    selectedUserConnections.TryAdd(item.Key, item.Value);
                }
            }

            return selectedUserConnections;
        }
    }
}