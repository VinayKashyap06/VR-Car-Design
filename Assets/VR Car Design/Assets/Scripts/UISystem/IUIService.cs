using UnityEngine;
using System.Collections;

namespace UISystem
{
    public interface IUIService
    {
        void SetMaterial(MaterialTypeEnum materialTypeEnum);
        void SetColor(ColorOptionsEnum colorOptionsEnum);
        void ShowMenu();
    }
}