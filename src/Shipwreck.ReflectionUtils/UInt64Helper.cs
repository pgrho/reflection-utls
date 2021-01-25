using System.ComponentModel;

#if USE_INTRINSIC
using System.Runtime.Intrinsics.X86;
#endif

namespace Shipwreck.ReflectionUtils
{
    public static class UInt64Helper
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static byte GetPopCount(this ulong value)
        {
            unchecked
            {
#if USE_INTRINSIC
                if (Popcnt.X64.IsSupported)
                {
                    return (byte)Popcnt.X64.PopCount(value);
                }
                if (Popcnt.IsSupported)
                {
                    return (byte)(Popcnt.PopCount((uint)value) + Popcnt.PopCount((uint)(value >> 32)));
                }
#endif
                value = value - ((value >> 1) & 0x5555555555555555UL);
                value = (value & 0x3333333333333333UL) + ((value >> 2) & 0x3333333333333333UL);
                return (byte)((((value + (value >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
            }
        }
    }
}
