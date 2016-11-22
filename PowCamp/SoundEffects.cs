using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class SoundEffects
    {
        public static SoundEffect errorSound;

        public static void load(ContentManager contentManager)
        {
            errorSound = contentManager.Load<SoundEffect>("SoundFX/error");
        }
    }
}
