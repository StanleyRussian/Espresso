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
            _context = new Entity.ContextContainer();
            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            Refresh();
        }

        private void Refresh()
        {
            NewCoffeeSort = new Entity.CoffeeSort();
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
                MessageBox.Show("Сохранение прошло успешно");
                Refresh();
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
