using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicB0t.Enums
{
    public enum ChatColorPresets
    {        
        Blue,
        Coral,
        DodgerBlue,
        SpringGreen,
        YellowGreen,
        Green,
        OrangeRed,
        Red,
        GoldenRod,
        HotPink,
        CadetBlue,
        SeaGreen,
        Chocolate,
        BlueViolet,
        Firebrick
    }

    public static class ChatColorPresetsExtensions
    {
        public static IEnumerable<ChatColorPresets> GetValues()
        {
            return Enum.GetValues(typeof(ChatColorPresets)).Cast<ChatColorPresets>();
        }
    }
}
