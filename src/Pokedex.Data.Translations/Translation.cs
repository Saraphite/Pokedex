using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Data.Translations
{
    public class Translation
    {
        /// <summary>
        /// The language of the translation.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// The translated text.
        /// </summary>
        public string Text { get; set; }

    }
}
