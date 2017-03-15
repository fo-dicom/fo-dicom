namespace Unity.IO.Compression {
    
    using System;
    using System.Runtime.Serialization;

#if !NETFX_CORE
    [Serializable] 
#endif // !FEATURE_NETCORE
    public sealed class InvalidDataException
#if NETFX_CORE
        : Exception
#else
        : SystemException
#endif
    {
        public InvalidDataException () 
            : base(SR.GetString(SR.GenericInvalidData)) {
        }

        public InvalidDataException (String message) 
            : base(message) {
        }
    
        public InvalidDataException (String message, Exception innerException) 
            : base(message, innerException) {
        }

#if !NETFX_CORE
        internal InvalidDataException (SerializationInfo info, StreamingContext context) : base(info, context) {
        }
#endif // !NETFX_CORE

    }
}
