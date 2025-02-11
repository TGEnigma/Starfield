﻿using System.IO;
using Starfield.IO.Serialization;

namespace Starfield.IO.Utilities
{
    /// <summary>
    /// Useful extension methods for <see cref="IBinarySerializable"/> instances.
    /// </summary>
    public static class IBinarySerializableExtensions
    {
        public static void Save( this IBinarySerializable @this, string filePath )
        {
            using ( var writer = new ObjectBinaryWriter( new MemoryStream(), Endianness.Little ) )
            {
                @this.Write( writer );
                using ( var fileStream = File.Create( filePath ) )
                {
                    writer.BaseStream.Position = 0;
                    writer.BaseStream.CopyTo( fileStream );
                }
            }
        }

        public static void Save( this IBinarySerializable @this, Stream stream, bool leaveOpen = true )
        {
            using ( var writer = new ObjectBinaryWriter( stream, leaveOpen, Endianness.Little ) )
                @this.Write( writer );
        }

        public static MemoryStream Save( this IBinarySerializable @this )
        {
            var stream = new MemoryStream();
            @this.Save( stream );
            stream.Position = 0;
            return stream;
        }
    }
}