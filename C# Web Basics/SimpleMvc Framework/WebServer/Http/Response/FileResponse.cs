﻿namespace WebServer.Http.Response
{
    using Enums;
    using Exceptions;

    public class FileResponse : HttpResponse
    {
        public FileResponse(HttpStatusCode statusCode, byte[] fileData)
        {
            this.ValidateStatusCode(statusCode);

            this.StatusCode = statusCode;
            this.FileData = fileData;

            this.Headers.Add(HttpHeader.ContentLength, this.FileData.Length.ToString());
            this.Headers.Add(HttpHeader.ContentDisposition, "attachment");
        }

        public byte[] FileData { get; }

        private void ValidateStatusCode(HttpStatusCode statusCode)
        {
            int statusCodeNumber = (int) statusCode;

            if (299 > statusCodeNumber && statusCodeNumber < 400)
            {
                throw new InvalidResponseException("File response need a status code above 300 and below 400");
            }
        }
    }
}