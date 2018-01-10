using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyNotes.Log.Models
{
    /// <summary>
    /// 日志内容
    /// </summary>
    public class FlashLogMessage
    {
        public string Message { get; set; }
        public FlashLogLevel Level { get; set; }
        public Exception Exception { get; set; }

    }
}