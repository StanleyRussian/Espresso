using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    public class NewCoffeeSortViewModel
    {
        protected Entity.ContextContainer _context;

        public NewCoffeeSortViewModel()
        {
            NewCoffeeSort = new Entity.CoffeeSort();
            _context = new Entity.ContextContainer();

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
        }

        public Entity.CoffeeSort NewCoffeeSort
        { get; private set; }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.CoffeeSorts.Add(NewCoffeeSort);
            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var x in ex.EntityValidationErrors)
                    foreach (var y in x.ValidationErrors)
                        MessageBox.Show(y.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
