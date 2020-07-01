using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        Service service = new Service();

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            EmployeeList = service.GetAllEmployees();
        }
        #region Properties
        private vwEmployee viewEmployee;
        public vwEmployee ViewEmployee
        {
            get
            {
                return viewEmployee;
            }
            set
            {
                viewEmployee = value;
                OnPropertyChanged("ViewEmployee");
            }
        }
        private List<vwEmployee> employeeList;
        public List<vwEmployee> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }
        #endregion

        private ICommand deleteEmployee;
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }
        }
        public void DeleteEmployeeExecute()
        {
            try
            {
                if (ViewEmployee != null)
                {
                    using (DAN_XLIIEntities1 context = new DAN_XLIIEntities1())
                    {
                        string jmbg = ViewEmployee.JMBG;
                        MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confrimation", MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            tblEmployee employeeToDelete = (from r in context.tblEmployees where r.JMBG == jmbg select r).First();
                            context.tblEmployees.Remove(employeeToDelete);
                            
                            context.SaveChanges();
                            //Logging.Log.WriteDelete(userToDelete);

                            EmployeeList = service.GetAllEmployees().ToList();
                        }
                        else
                        {
                            MessageBox.Show("Cannot delete the Employee");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteEmployeeExecute()
        {
            if (ViewEmployee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

       

        private ICommand editEmployee;
        public ICommand EditEmployee
        {
            get
            {
                if (editEmployee == null)
                {
                    editEmployee = new RelayCommand(param => EditEmployeeExecute(), param => CanEditUserExecute());

                }
                return editEmployee;
            }
        }

        private void EditEmployeeExecute()
        {
            try
            {
                EditEmployee editEmployee = new EditEmployee (viewEmployee);
                
                editEmployee.ShowDialog();
                
                if ((editEmployee.DataContext as EditEmployeeViewModel).IsUpdated == true)
                {
                    
                    EmployeeList = service.GetAllEmployees();
                    
                    main.DataGridEmployees.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditEmployeeExecute()
        {
            return true;
        }
        private ICommand addEmployee;
        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee == null)
                {
                    addEmployee = new RelayCommand(param => AddUserExecute(), param => CanAddEmployeeExecute());

                }
                return addEmployee;
            }
        }

        private void AddUserExecute()
        {
            try
            {
                AddEmployee addEmployee = new AddEmployee();
                //opening new window
                addEmployee.ShowDialog();
                //checking for updates
                if ((this.addEmployee.DataContext as AddEmployeeViewModel).IsUpdateEmployee == true)
                {
                    //refresing list => including added user
                    EmployeeList = service.GetAllEmployees();

                    main.DataGridEmployees.Items.SortDescriptions.Add(new SortDescription("Surname", ListSortDirection.Ascending));
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddEmployeeExecute()
        {
            return true;
        }

    }
}
