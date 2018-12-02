﻿using System;
using System.Runtime.Serialization;

namespace SimpleFtp
{
    [Serializable]
    public class DownloadErrorException : Exception
    {
        public DownloadErrorException()
        {
        }

        public DownloadErrorException(string message) : base(message)
        {
        }

        public DownloadErrorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DownloadErrorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}