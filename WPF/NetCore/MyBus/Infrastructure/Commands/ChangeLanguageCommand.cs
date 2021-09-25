using System.Globalization;
using MyBus.Infrastructure.Commands.Base;

namespace MyBus.Infrastructure.Commands
{
    public class ChangeLanguageCommand : Command
    {
        public override bool CanExecute(object parameter) => true;
        public override void Execute(object parameter)
        {
            App.Language = (CultureInfo) parameter;
        }   
    }
}
