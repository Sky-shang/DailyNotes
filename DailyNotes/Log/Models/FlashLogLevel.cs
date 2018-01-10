using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyNotes.Log.Models
{
    /// <summary>
    /// 日志等级
    /// </summary>
    public enum FlashLogLevel
    {
        Debug,
        Info,
        Error,
        Warn,
        Fatal
    }
}