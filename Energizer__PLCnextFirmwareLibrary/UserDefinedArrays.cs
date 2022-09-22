using System;
using System.Iec61131Lib;
using System.Runtime.InteropServices;
using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

namespace Energizer__PLCnextFirmwareLibrary
{
    //DataType is 'INT' in PLCnext and 'short' in C#
    [Array(1), ArrayDimension(0, ArrayProperties.LowerBound, ArrayProperties.UpperBound), DataType("INT")]
    [StructLayout(LayoutKind.Explicit, Size = ArrayProperties.ByteSize)]
    public struct ShortArrayFB
    {
        // Helper containing constants to have a
        // clear and maintainable definition for boundaries and size
        public struct ArrayProperties
        {
            public const int LowerBound = 0;     // must not necessarily being zero, it also can be negative
            public const int UpperBound = 99;       // IEC61131 representation is : userArray : ARRAY[-10..9] OF DINT     (* size == 20 *)

            // the size must be changed to the correct size of your elements times the amount of elements
            public const int ByteSize = (UpperBound - LowerBound + 1) * sizeof(short);
        }

        public const short ByteSize = ArrayProperties.ByteSize;

        // Fields
        // The field "Anchor" defines the beginning of the array.
        [FieldOffset(0)]
        // The Anchor's data type is the child data type of the array
        public short Anchor;

        // The constants LB and UB define the upper and lower bound. Boundaries will be checked by using them.
        public const short LB = ArrayProperties.LowerBound;

        public const short UB = ArrayProperties.UpperBound;

        public short this[short index]
        {
            get
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
                {
                    unsafe
                    {
                        fixed (short* pValue = &Anchor)
                        {
                            short result = *(pValue + index);
                            return result;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
                {
                    unsafe
                    {
                        fixed (short* pValue = &Anchor)
                        {
                            *(pValue + index) = value;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }

    //DataType is 'TIME' in PLCnext and 'int' in C#
    [Array(1), ArrayDimension(0, ArrayProperties.LowerBound, ArrayProperties.UpperBound), DataType("TIME")]
    [StructLayout(LayoutKind.Explicit, Size = ArrayProperties.ByteSize)]
    public struct IntArrayFB
    {
        // Helper containing constants to have a
        // clear and maintainable definition for boundaries and size

        public struct ArrayProperties
        {
            public const int LowerBound = 0;     // must not necessarily being zero, it also can be negative
            public const int UpperBound = 99;       // IEC61131 representation is : userArray : ARRAY[-10..9] OF DINT     (* size == 20 *)

            // the size must be changed to the correct size of your elements times the amount of elements
            public const int ByteSize = (UpperBound - LowerBound + 1) * sizeof(int);
        }
        public const int ByteSize = ArrayProperties.ByteSize;

        // Fields
        // The field "Anchor" defines the beginning of the array.
        [FieldOffset(0)]
        // The Anchor's data type is the child data type of the array
        public int Anchor;

        // The constants LB and UB define the upper and lower bound. Boundaries will be checked by using them.
        public const int LB = ArrayProperties.LowerBound;

        public const int UB = ArrayProperties.UpperBound;

        public int this[int index]
        {
            get
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
                {
                    unsafe
                    {
                        fixed (int* pValue = &Anchor)
                        {
                            int result = *(pValue + index);
                            return result;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
                {
                    unsafe
                    {
                        fixed (int* pValue = &Anchor)
                        {
                            *(pValue + index) = value;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }

    //DataType is 'STRING' in PLCnext and 'IecString80' in C#
    [Array(1), ArrayDimension(0, ArrayProperties.LowerBound, ArrayProperties.UpperBound), DataType("STRING")]
    [StructLayout(LayoutKind.Explicit, Size = ArrayProperties.ByteSize)]
    public struct StringArrayFB
    {
        // Helper containing constants to have a
        // clear and maintainable definition for boundaries and size
        public struct ArrayProperties
        {
            public const int LowerBound = 0;     // must not necessarily being zero, it also can be negative
            public const int UpperBound = 99;       // IEC61131 representation is : userArray : ARRAY[-10..9] OF DINT     (* size == 20 *)

            // the size must be changed to the correct size of your elements times the amount of elements, 86 is the size of a IecString80
            public const int ByteSize = (UpperBound - LowerBound + 1) * 86;
        }

        public const int ByteSize = ArrayProperties.ByteSize;

        // Fields
        // The field "Anchor" defines the beginning of the array.
        [FieldOffset(0)]
        // The Anchor's data type is the child data type of the array
        public IecString80 Anchor;

        // The constants LB and UB define the upper and lower bound. Boundaries will be checked by using them.
        public const int LB = ArrayProperties.LowerBound;

        public const int UB = ArrayProperties.UpperBound;

        //
        // Summary:
        //     Running constructor for IecStringEx object, removes use of 'unsafe' outside of class
        //
        public void Construct()
        {
            unsafe
            {
                fixed (IecString80* data = &Anchor)
                {
                    for (int i = 0; i <= (UB - LB); i++)
                    {
                        data[i].ctor();
                    }
                }
            }
        }

        //
        // Summary:
        //     Calls to initialize a string within IecString, this avoids the use of 'unsafe' outside of the class
        //
        public void InitStr(int index, string s)
        {
            unsafe
            {
                fixed (IecString80* pValue = &Anchor)
                {
                    (*(pValue + index)).s.Init(s);
                }
            }
        }

        public IecString80 this[int index]
        {
            get
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
                {
                    unsafe
                    {
                        fixed (IecString80* pValue = &Anchor)
                        {
                            IecString80 result = *(pValue + index);
                            return result;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
                {
                    unsafe
                    {
                        fixed (IecString80* pValue = &Anchor)
                        {
                            *(pValue + index) = value;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }

    [Array(1), ArrayDimension(0, ArrayProperties.LowerBound, ArrayProperties.UpperBound), DataType("BOOL")]
    [StructLayout(LayoutKind.Explicit, Size = ArrayProperties.ByteSize)]
    public struct BoolArrayFB
    {
        // Helper containing constants to have a
        // clear and maintainable definition for boundaries and size

        public struct ArrayProperties
        {
            public const int LowerBound = 0;     // must not necessarily being zero, it also can be negative
            public const int UpperBound = 99;       // IEC61131 representation is : userArray : ARRAY[-10..9] OF DINT     (* size == 20 *)

            // the size must be changed to the correct size of your elements times the amount of elements
            public const int ByteSize = (UpperBound - LowerBound + 1) * sizeof(bool);
        }
        public const short ByteSize = ArrayProperties.ByteSize;

        // Fields
        // The field "Anchor" defines the beginning of the array.
        [FieldOffset(0)]
        // The Anchor's data type is the child data type of the array
        public bool Anchor;

        // The constants LB and UB define the upper and lower bound. Boundaries will be checked by using them.
        public const short LB = ArrayProperties.LowerBound;

        public const short UB = ArrayProperties.UpperBound;



        public bool this[short index]
        {
            get
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
                {
                    unsafe
                    {
                        fixed (bool* pValue = &Anchor)
                        {
                            bool result = *(pValue + index);
                            return result;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
                {
                    unsafe
                    {
                        fixed (bool* pValue = &Anchor)
                        {
                            *(pValue + index) = value;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
