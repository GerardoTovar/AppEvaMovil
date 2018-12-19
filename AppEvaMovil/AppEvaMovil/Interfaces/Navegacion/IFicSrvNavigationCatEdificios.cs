using System;
using System.Collections.Generic;
using System.Text;

namespace AppEvaMovil.Interfaces.Navegacion
{
    public interface IFicSrvNavigationCatEdificios
    {
        /*METODOS PARA LA NAVEGACION ENTRE VIEWS DE LA APP*/
        void FicMetNavigateTo<FicTDestinationViewModel>(object FicNavigationContext = null);
        void FicMetNavigateTo(Type FicDestinationType, object FicNavigationContext = null);
        void FicMetNavigateBack();
    }//INTERFACE
}//NAMESPACE