using System;
using System.Collections.Generic;
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
    class EditEmployeeViewModel : ViewModelBase
    {
        EditEmployee editEmployee;
        Service service = new Service();

        public EditEmployeeViewModel(EditEmployee editEmployeeOpen, vwEmployee vwEmployee)
        {
            editEmployee = editEmployeeOpen;
            employee = new tblEmployee();
            LocationList = service.GetAllLocations();
        }
        #region Properties

        private bool isUpdateEmployee;
        public bool IsUpdateEmployee
        {
            get
            {
                return isUpdateEmployee;
            }
            set
            {
                isUpdateEmployee = value;
            }
        }

        private tblEmployee employee;
        public tblEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }


        private List<tblLocation> locationList;
        public List<tblLocation> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }
        private tblLocation location;
        public tblLocation Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }
        #endregion

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());

                }
                return save;
            }

        }
        private void SaveExecute()
        {
            try
            {
                using (DAN_XLIIEntities1 context = new DAN_XLIIEntities1())
                {
                    tblEmployee newEmployee = new tblEmployee();
                    Random rnd = new Random();

                    newEmployee.FirstName = Employee.FirstName;
                    newEmployee.Surname = Employee.Surname;
                    newEmployee.JMBG = Employee.JMBG;
                    newEmployee.GenderID = Employee.GenderID;
                    newEmployee.LocationID = Location.LocationID;
                    newEmployee.SectorID = newEmployee.SectorID;

                    context.tblEmployees.Add(newEmployee);

                    context.SaveChanges();
                   
                }
                
                IsUpdateEmployee = true;
              
                editEmployee.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Error occured. Make sure that you have provided valid JMBG. Please fix the problems and try again.");
            }
        }
        private bool CanSaveExecute()
        {
            //all text boxes must be filled with data
            if (String.IsNullOrEmpty(employee.FirstName) || String.IsNullOrEmpty(employee.Surname) || String.IsNullOrEmpty(employee.JMBG) || Location == null || Employee.JMBG.Count() != 13)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());

                }
                return close;
            }
        }
        private void CloseExecute()
        {
            try
            {
               
                editEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            
            return true;
        }


    }
}
