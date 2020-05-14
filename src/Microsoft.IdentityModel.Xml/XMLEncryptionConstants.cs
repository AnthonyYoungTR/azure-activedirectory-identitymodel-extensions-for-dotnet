﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Xml
{
    /// <summary>
    /// Constants for XML Encryption
    /// Definitions for namespace, attributes and elements as defined in http://www.w3.org/TR/xmlenc-core1/
    /// </summary>
    internal static class XmlEncryptionConstants
    {
#pragma warning disable 1591
        public const string Namespace = "http://www.w3.org/2009/xmlenc11#";

        public const string Prefix = "xenc11";

        public static class Attributes
        {
            public const string Algorithm = "Algorithm";
            public const string Encoding = "Encoding";
            public const string Id = "Id";
            public const string MimeType = "MimeType";
            public const string Recipient = "Recipient";
            public const string Type = "Type";
            public const string Uri = "URI";
        }

        public static class Elements
        {
            public const string CarriedKeyName = "CarriedKeyName";
            public const string CipherData = "CipherData";
            public const string CipherReference = "CiperReference";
            public const string CipherValue = "CipherValue";
            public const string DataReference = "DataReference";
            public const string EncryptedData = "EncryptedData";
            public const string EncryptedKey = "EncryptedKey";
            public const string EncryptionMethod = "EncryptionMethod";
            public const string EncryptionProperties = "EncryptionProperties";
            public const string KeyReference = "KeyReference";
            public const string KeySize = "KeySize";
            public const string OaepParams = "OAEPparams";
            public const string Recipient = "Recipient";
            public const string ReferenceList = "ReferenceList";
        }

        public static class EncryptedDataTypes
        {
            public const string Element = Namespace + "Element";
            public const string Content = Namespace + "Content";
#pragma warning restore 1591
        }
    }
}