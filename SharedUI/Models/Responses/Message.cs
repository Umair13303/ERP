using SharedUI.Models.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Responses
{
    public class Message
    {
        public static string serverResponse(int? code)
        {
            return code switch
            {
                100 => "Continue",
                101 => "Switching Protocols",
                102 => "Processing",


                200 => "OK - Request successful",
                201 => "Created - Resource created successfully",
                202 => "Accepted - Request accepted for processing",
                203 => "Non-Authoritative Information",
                204 => "No Content - Request successful, but no content to return",
                205 => "Reset Content",
                206 => "Partial Content",

                300 => "Multiple Choices",
                301 => "Moved Permanently",
                302 => "Found (Moved Temporarily)",
                303 => "See Other",
                304 => "Not Modified",
                307 => "Temporary Redirect",
                308 => "Permanent Redirect",

                400 => "Bad Request - The server cannot process the request due to client error",
                401 => "Unauthorized - Authentication is required",
                402 => "Payment Required",
                403 => "Forbidden - You do not have permission to access this resource",
                404 => "Not Found - The requested resource could not be found",
                405 => "Method Not Allowed",
                406 => "Not Acceptable",
                408 => "Request Timeout",
                409 => "Conflict",
                410 => "Gone",
                413 => "Payload Too Large",
                415 => "Unsupported Media Type",
                422 => "Unprocessable Entity",
                429 => "Too Many Requests - Rate limit exceeded",

                500 => "Internal Server Error - Something went wrong on our end",
                501 => "Not Implemented",
                502 => "Bad Gateway",
                503 => "Service Unavailable - Server is temporarily overloaded or down",
                504 => "Gateway Timeout",
                505 => "HTTP Version Not Supported",

                _ => "Unknown HTTP Response Code"
            };
        }
        public static readonly List<DDLItem> ddlEmployee = new List<DDLItem>
        {
            new DDLItem { Id = 0, Description = "Attach Employee On Later Stage" }
        };
    }
}