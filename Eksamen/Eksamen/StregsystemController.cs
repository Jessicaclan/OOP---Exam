using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamen
{
    public class StregsystemController
    {
        private IStregsystem _stregsystem;
        private IStregsystemUI _ui;
        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            _ui = ui;
            _ui.CommandEntered += OnCommandEntered; //Subscriber
        }
        private void ParseAdminCommand(string[] commandString)
        {
            switch (commandString[0])
            {
                case ":quit":
                    _ui.Close();
                    break;
                case ":q":
                    _ui.Close();
                    break;
                case ":producton/off":
                    try
                    {
                        _stregsystem.ChangeProductStatus(_stregsystem.GetProductByID(int.Parse(commandString[1])));
                    }
                    catch (ProductNotFoundException e)
                    {
                        _ui.DisplayProductNotFound(e.Message);
                    }
                    break;
                case ":addcredits":
                    try
                    {
                        _ui.DisplayDesposit(_stregsystem.AddCreditsToAccount(_stregsystem.GetUserByUsername(commandString[1]), int.Parse(commandString[2])));   
                    }
                    catch (UserNotFoundException e)
                    {
                        _ui.DisplayUserNotFound(e.Message);
                    }
                    break;
                case ":crediton/off":
                    try
                    {
                        _stregsystem.ChangeCreditStatus(_stregsystem.GetProductByID(int.Parse(commandString[1])));
                    }
                    catch (ProductNotFoundException e)
                    {
                        _ui.DisplayProductNotFound(e.Message);
                    }
                    break;
                default:
                    _ui.DisplayAdminCommandNotFoundMessage(commandString[0]);
                    break;
            }
        }
        private void ParseUserCommand(string[] commandString)
        {
            switch (commandString.Length)
            {
                case 1: //Display user
                    try
                    {
                        _ui.DisplayUserInfo(_stregsystem.GetUserByUsername(commandString[0]));
                    }
                    catch(UserNotFoundException e)
                    {
                        _ui.DisplayUserNotFound(e.Message);
                    }
                    break;

                case 2: //Buy Product
                    try
                    {
                        _ui.DisplayUserBuysProduct(_stregsystem.BuyProduct(_stregsystem.GetUserByUsername(commandString[0]), _stregsystem.GetActiveProductByID(int.Parse(commandString[1]))));
                    }
                    catch (UserNotFoundException e)
                    {
                        _ui.DisplayUserNotFound(e.Message);
                    }
                    catch (InsufficientCreditsException) 
                    {
                        _ui.DisplayInsufficientCash(_stregsystem.GetUserByUsername(commandString[0]), _stregsystem.GetActiveProductByID(int.Parse(commandString[1])));
                    }
                    catch (ProductNotFoundException e)
                    {
                        _ui.DisplayProductNotFound(e.Message);
                    }
                    break;

                case 3: //Buy Multiple products 
                    try
                    {
                        MultiBuy(commandString);
                    }
                    catch (UserNotFoundException e)
                    {
                        _ui.DisplayUserNotFound(e.Message);
                    }
                    catch (InsufficientCreditsException) 
                    {
                        _ui.DisplayInsufficientCash(_stregsystem.GetUserByUsername(commandString[0]), _stregsystem.GetActiveProductByID(int.Parse(commandString[1])));
                    }
                    catch (ProductNotFoundException e)
                    {
                        _ui.DisplayProductNotFound(e.Message);
                    }
                    break;
                default:
                    _ui.DisplayAdminCommandNotFoundMessage(commandString[0]);
                    break;
            }
        }
        private void ParseCommand(string command)
        {
            command.Trim();
            string[] commandString = command.Split(" ");

            if (commandString[0][0].Equals(':')) 
            {
                ParseAdminCommand(commandString);
            }
            else
            {
                ParseUserCommand(commandString);
            }
            
        }
        private void OnCommandEntered(string command) //Event handler
        {
            ParseCommand(command);
        }
        private void MultiBuy(string[] commandString)
        {
            for (int i = 0; i < int.Parse(commandString[1]); i++)
            {
                BuyTransaction multiBuy = _stregsystem.BuyProduct(_stregsystem.GetUserByUsername(commandString[0]), _stregsystem.GetActiveProductByID(int.Parse(commandString[2])));

                if (i == (int.Parse(commandString[1]) - 1))
                {
                    _ui.DisplayUserBuysProduct(int.Parse(commandString[1]), multiBuy);
                }
            }
        }
    }
}
