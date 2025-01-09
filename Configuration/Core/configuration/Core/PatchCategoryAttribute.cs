using System;

namespace GHTweaks.Configuration.Core
{
    internal class PatchCategoryAttribute : Attribute
    {
        public string PatchCategory { get; set; }


        public PatchCategoryAttribute() { }

        public PatchCategoryAttribute(string patchCategory) => PatchCategory = patchCategory;
    }
}
