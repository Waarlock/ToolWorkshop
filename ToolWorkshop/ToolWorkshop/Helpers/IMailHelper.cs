﻿using ToolWorkshop.Common;

namespace ToolWorkshop.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
